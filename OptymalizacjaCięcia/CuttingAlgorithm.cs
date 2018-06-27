using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptymalizacjaCięcia
{
        //   Co prawda problem jest NP-trudny,
        // ale poniższy algorytm działa przyzwoicie na danych testowych. Nie musi być optymalny - drobne
        // straty są dopuszczalne. Poniższy algorytm jest zachłanny - za każdym razem, kiedy ma wybrać element
        // (lub parę elementów, jeśli dopuszczone jest stykowanie), to wybiera w ten sposób, żeby po wycięciu
        // pozycji (lub jej części), element z magazynu był możliwi`e jak najkrótszy.
    class CuttingAlgorithm
    {
        const int customLength1 = 6000;
        const int customLength2 = 12000;
        const int customLength3 = 15000;

        // Otrzymuje listę elementów do wycięcia o jednym profilu, 
        // listę elemntów  magazynu o tym samym profilu i informację, czy można stykować.
        // Jeśli dopuszczone jest stykowanie, to może wyciąc część potrzebnej pozycji
        // z jednego elementu, a drugą część z innego elementu, przy czym styk może znajdować
        // się w 1/3 lub 1/4 pozycji (+- parę milimetrów).
        // Algorytm najpierw uzupełnia magazyn tak, aby łączna długość w magazynie była większa, niż
        // łączna długość pozycji do wycięcia, a następnie próbuje wygenerować listę rozkrojową dla danego
        // stanu magazynu. Dodaje po jednym elemencie do magazynu, dopóki stworzenie listy rozkrojowej się nie powiedzie
        // Zwraca listę rozkrojową.
        public static SortedDictionary<StoredElement, List<CommissionElement>> 
            Solution(ref List<StoredElement> store, List<CommissionElement> elementsToCut, bool bonding)
        {
            SortedDictionary<StoredElement, List<CommissionElement>> cutAssignment =
                new SortedDictionary<StoredElement, List<CommissionElement>>();
            bool success = false;
            int storeLength = GetElementsLength(store);
            int elementsToCutLength = GetElementsLength(elementsToCut);
            
            if (bonding)
            {
                if (!AddLargeElements(ref storeLength, ref store, ref elementsToCut, 
                    3 * customLength3 / 2, 3 * customLength3 / 2))
                {
                    return null;
                }
            }
            else
            {
                if (!AddLargeElements(ref storeLength, ref store, ref elementsToCut, customLength3, customLength2))
                {
                    return null;
                }
            }
            

            while (storeLength < elementsToCutLength)
            {
                store.Add(new StoredElement(elementsToCut.First().GetProfile(), "", customLength2, "", store.Count));
                storeLength += customLength2;
            }

            while (!success)
            {
                cutAssignment = new SortedDictionary<StoredElement, List<CommissionElement>>();
                List<StoredElement> storeCopy = store.Select(x => new StoredElement(x)).ToList();
                List<CommissionElement> elementsToCutCopy = 
                    elementsToCut.Select(x => new CommissionElement(x)).ToList();

                if (bonding)
                {
                    success = TryAssignment(ref cutAssignment, storeCopy, elementsToCutCopy, true);
                }
                else
                {
                    success = TryAssignment(ref cutAssignment, storeCopy, elementsToCutCopy, false);
                }
                
                if (!success)
                {
                    store.Add(new StoredElement("", "", customLength2, "", store.Count));
                }
                else
                {
                    store = storeCopy;
                }
            }

            store.OrderBy(x => x.GetLength());
            try
            {
               ExcelWriter.writeProfileToStoreFile(StartupWindow.storeFile, store);
            }
            catch (Exception e)
            {
                e.ToString();
            }
           
            return cutAssignment;
        }

        private static bool AddLargeElements(ref int storeLength, ref List<StoredElement> store, 
            ref List<CommissionElement> elementsToCut, int maxLength, int minToAddLargest)
        {
            foreach (CommissionElement e in elementsToCut)
            {
                if (e.GetLength() > maxLength)
                {
                    return false;
                }

                if (e.GetLength() > minToAddLargest)
                {
                    store.Add(new StoredElement(e.GetProfile(), e.GetElementType(), customLength3, "", store.Count));
                    storeLength += customLength3;
                }
            }

            return true;
        }
        
        private static int GetElementsLength(IEnumerable<Element> elementList)
        {
            int sum = 0;
            foreach (Element e in elementList)
            {
                sum += e.GetLength();
            }

            return sum;
        }

        // Metoda próbuje wygenerować listę rozkrojową.  
        private static bool TryAssignment(ref SortedDictionary<StoredElement, List<CommissionElement>> cutAssignment,
            List<StoredElement> store, List<CommissionElement> elementsToCut, bool bonding)
        {
            int secondIndex;

            while (elementsToCut.Any<CommissionElement>())
            {
                Tuple<int, int, int, int> indexLength = new Tuple<int, int, int, int>(-1, -1, -1, -1);
                CommissionElement largestToCut = elementsToCut[elementsToCut.Count - 1];
                int len = largestToCut.GetLength();
                
                int ind = FindSmallestPossible(store, len, -1);

                if (bonding)
                {
                    indexLength = FindSmallestPair(store, len);
                }

                if (ind == -1 && !bonding)
                {
                    return false;
                }

                if (ind == -1 && (indexLength.Item1 == -1 || indexLength.Item2 == -1))
                {
                    return false;
                }

                if (ind >= 0 && ((indexLength.Item1 == -1 || indexLength.Item2 == -1) ||
                    (store[ind].GetLength() - len <= store[indexLength.Item1].GetLength() - indexLength.Item3 &&
                     store[ind].GetLength() - len <= store[indexLength.Item2].GetLength() - indexLength.Item4)))
                {
                    if (!cutAssignment.ContainsKey(store[ind]))
                    {
                        cutAssignment.Add(store[ind], new List<CommissionElement>());
                    }
                    cutAssignment[store[ind]].Add(largestToCut);

                    elementsToCut.Remove(largestToCut);

                    int curr_len = store[ind].GetLength();

                    store[ind].SetLength(curr_len - len);
                    if (curr_len == len)
                    {
                        store.Remove(store[ind]);
                    }
                }
                else
                {
                    if (!cutAssignment.ContainsKey(store[indexLength.Item1]))
                    {
                        cutAssignment.Add(store[indexLength.Item1], new List<CommissionElement>());
                    }
                    cutAssignment[store[indexLength.Item1]].Add(
                        new CommissionElement("("+largestToCut.GetLength().ToString()+")", "", indexLength.Item3, 0));

                    if (!cutAssignment.ContainsKey(store[indexLength.Item2]))
                    {
                        cutAssignment.Add(store[indexLength.Item2], new List<CommissionElement>());
                    }
                    cutAssignment[store[indexLength.Item2]].Add(
                        new CommissionElement("(" + largestToCut.GetLength().ToString() + ")", "", indexLength.Item4, 0));

                    elementsToCut.Remove(largestToCut);

                    secondIndex = indexLength.Item2;

                    int curr_len = store[indexLength.Item1].GetLength();
                    store[indexLength.Item1].SetLength(curr_len - indexLength.Item3);
                    if (curr_len == indexLength.Item3)
                    {
                        store.Remove(store[indexLength.Item1]);

                        if (indexLength.Item1 < indexLength.Item2)
                        {
                            secondIndex = indexLength.Item2 - 1;
                        }
                        else
                        {
                            secondIndex = indexLength.Item2;
                        }
                    }

                    curr_len = store[secondIndex].GetLength();
                    
                    store[secondIndex].SetLength(curr_len - indexLength.Item4);
                    if (curr_len == indexLength.Item4)
                    {
                        store.Remove(store[secondIndex]);
                    }
                }
            }

            return true;
        }

        private static int FindSmallestPossible(IEnumerable<Element> elements, int length, int indToAvoid)
        {
            int diff;
            int i = 0;
            int min_diff = 1000000;
            int ind = -1;

            foreach (Element e in elements)
            {
                if (i != indToAvoid)
                {
                    diff = e.GetLength() - length;
                    if (diff >= 0 && diff < min_diff)
                    {
                        ind = i;
                        min_diff = diff;
                    }
                }
                
                ++i;
            }
            return ind;
        }

        // Jeśli dopuszczone jest stykowanie, to dla każdego elementu do wycięcia zostanie sprawdzone, czy
        // jest możliwe wyięcie danego elementu z pewnych dwóch elementów magazynu. Jeśli jest to możliwe
        // i bardziej opłacalne, niż wycięcie z jednego elementu, to zwraca krotkę postaci:
        // (indeks pierwszego elementu magazynu z którego wycina, indeks drugiego leementu, 
        //  długość częsci wycinanej z pierwszego, długość częsci wycinanej z drugiego)
        private static Tuple<int, int, int, int> FindSmallestPair(List<StoredElement> elements, int length)
        {
            int len1 = -1;
            int len2 = -1;
            int bestInd1 = -1;
            int bestInd2 = -1;

            if (length % 3 == 0)
            {
                TryBonding(elements, length / 3, 2 * length / 3, ref bestInd1, ref bestInd2, ref len1, ref len2);
            }
            else if (length % 3 == 1)
            {
                TryBonding(elements, (length - 1) / 3, (2 * length + 1) / 3,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
                TryBonding(elements, (length + 2) / 3, 2 * (length - 1) / 3,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
            }
            else
            {
                TryBonding(elements, (length - 2) / 3, 2 * (length + 1) / 3,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
                TryBonding(elements, (length + 1) / 3, (2 * length - 1) / 3,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
            }

            if (length % 4 == 0)
            {
                TryBonding(elements, length / 4, 3 * length / 4, ref bestInd1, ref bestInd2, ref len1, ref len2);
            }
            else if (length % 4 == 1)
            {
                TryBonding(elements, (length - 1) / 4, (3 * length + 1) / 4,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
                TryBonding(elements, (length + 3) / 4, (3 * (length - 1)) / 4,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
            }
            else if (length % 4 == 2)
            {
                TryBonding(elements, (length + 2) / 4, (3 * length - 2) / 4,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
                TryBonding(elements, (length - 2) / 4, (3 * length + 2) / 4,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
            }
            else
            {
                TryBonding(elements, (length - 3) / 4, 3 * (length + 1) / 4,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
                TryBonding(elements, (length + 1) / 4, (3 * length - 1) / 4,
                    ref bestInd1, ref bestInd2, ref len1, ref len2);
            }

            return Tuple.Create<int, int, int, int>(bestInd1, bestInd2, len1, len2);
        }

        private static void TryBonding(List<StoredElement> elements, int length1, int length2,
            ref int bestInd1, ref int bestInd2, ref int len1, ref int len2)
        {
            int ind1, ind2;
            int min_len;

            ind1 = FindSmallestPossible(elements, length1, -1);
            if (ind1 >= 0)
            {
                ind2 = FindSmallestPossible(elements, length2, ind1);
                if (ind2 >= 0)
                {
                    if (bestInd1 == -1 || bestInd2 == -1)
                    {
                        min_len = Math.Min(elements[ind1].GetLength(), elements[ind2].GetLength());
                    }
                    else
                    {
                        min_len = Math.Min(Math.Min(elements[ind1].GetLength(), elements[ind2].GetLength()),
                            Math.Min(elements[bestInd1].GetLength(), elements[bestInd2].GetLength()));
                    }
                    if (elements[ind1].GetLength() == min_len || elements[ind2].GetLength() == min_len)
                    {
                        bestInd1 = ind1;
                        bestInd2 = ind2;
                        len1 = length1;
                        len2 = length2;
                    }
                }
            }
        }
    }
}

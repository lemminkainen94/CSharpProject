using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/* Co by wypadało zrobić:

2. Żeby program działał jak należy, to musi mieć plik z magazynem i musi czytać plik z listą do wycięcia.
Tylko jak to parsować: Ustalanie po nazwie kolumny chyba odpada, bo nazwy są dowolne 
Chyba profil jest dobrym punktem zaczepinia, ale o to trzeba się jeszcze dopytać: może jakiś regex przejdzie
*/
namespace OptymalizacjaCięcia
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartupWindow());
        }

        public static void Compute(string storeFile, string commissionFile)
        {
            int profileNumber = 0;
            
            SortedDictionary<string, List<StoredElement>> store = new SortedDictionary<string, List<StoredElement>>();
            SortedDictionary<string, List<CommissionElement>> commission = 
                new SortedDictionary<string, List<CommissionElement>>();

            ExcelReader.ReadAllCellValues(storeFile, true, ref store, ref commission);
            ExcelReader.ReadAllCellValues(commissionFile, false, ref store, ref commission);

            

            ExcelWriter.UpdateStore(storeFile);

            foreach (KeyValuePair<string, List<CommissionElement>> p in commission)
            {
                p.Value.Sort();

                if (!store.ContainsKey(p.Key))
                {
                    store.Add(p.Key, new List<StoredElement>());
                }
                List<StoredElement> storedMatchingProfile = store[p.Key];

                SortedDictionary<StoredElement, List<CommissionElement>> assignment =
                    CuttingAlgorithm.Solution(ref storedMatchingProfile, p.Value, StartupWindow.bonding);

                CutAssignmentWriter.PrintCutAssignment(profileNumber, assignment);
                ++profileNumber;
            }
        }
    }
}

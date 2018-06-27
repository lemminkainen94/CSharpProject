using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OptymalizacjaCięcia
{
    class CutAssignmentWriter
    {
        public static void PrintCutAssignment(int profileNumber,
            SortedDictionary<StoredElement, List<CommissionElement>> cutAssignment)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(StartupWindow.cutAssignmentFile,
                    (profileNumber == 0) ? false : true))
            {
                Regex rgx = new Regex(@"^[(][0-9]+[)]$");

                if (cutAssignment.Any())
                {
                    file.WriteLine("Profil: " + cutAssignment.First().Key.GetProfile());
                    file.WriteLine();
                }

                foreach (KeyValuePair<StoredElement, List<CommissionElement>> p in cutAssignment)
                {
                    if (p.Key.GetDelivery() != "")
                    {
                        file.Write(p.Key.GetDelivery() + " -> ");
                    }
                    file.Write(p.Key.GetInitialLength() + " ->");
                    foreach (CommissionElement e in p.Value)
                    {
                        file.Write(" " + e.GetLength());
                        if (rgx.IsMatch(e.GetProfile()))
                        {
                            file.Write(e.GetProfile());
                        }
                    }
                    file.Write(" zostaje " + p.Key.GetLength());
                    file.WriteLine();
                }

                for (int i = 0; i != 4; ++i)
                {
                    file.WriteLine();
                }
            }
        }
    }
}

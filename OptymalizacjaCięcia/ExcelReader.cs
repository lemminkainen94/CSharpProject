using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
using System.Threading.Tasks;

namespace OptymalizacjaCięcia
{
    public class ExcelReader
    {
        public static void ReadAllCellValues(string fileName, bool storeFile,
            ref SortedDictionary<string, List<StoredElement>> storedElements,
            ref SortedDictionary<string, List<CommissionElement>> commissionElements)
        {
            using (
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                bool firstRow = true;
                LinkedList<StoredElement> store = new LinkedList<StoredElement>();
                LinkedList<CommissionElement> commission = new LinkedList<CommissionElement>();
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Row))
                    {
                        reader.ReadFirstChild();

                        string profile = "";
                        string type = "";
                        string delivery = "";
                        int len = 0;
                        int count = 0;
                        int listing = 0;
                        do
                        {
                            if (reader.ElementType == typeof(Cell))
                            {
                                Cell c = (Cell)reader.LoadCurrentElement();

                                if (c != null)
                                {
                                    string cellValue = "";

                                    string address = c.CellReference.ToString();
                                    string headerAddress = address[0] + "1";
                                    string value = null;

                                    value = GetCellValue(worksheetPart, workbookPart, headerAddress);

                                    if (c.DataType != null && c.DataType == CellValues.SharedString)
                                    {
                                        SharedStringItem ssi = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(c.CellValue.InnerText));

                                        cellValue = ssi.Text.Text;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            cellValue = c.CellValue.InnerText;
                                        }
                                        catch (NullReferenceException exception)
                                        {

                                        }
                                    }

                                    if (!firstRow)
                                    {
                                        switch (value)
                                        {
                                            case "Profil":
                                                profile = cellValue;
                                                break;
                                            case "Gatunek":
                                                type = cellValue;
                                                break;
                                            case "Długość":
                                                Int32.TryParse(cellValue.Split('.')[0], out len);
                                                break;
                                            case "Sztuk":
                                                Int32.TryParse(cellValue.Split('.')[0], out count);
                                                break;
                                            case "Nr Dostawy":
                                                delivery = cellValue;
                                                break;
                                            case "Pozycja":
                                                Int32.TryParse(cellValue.Split('.')[0], out listing);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {

                            }
                        } while (reader.ReadNextSibling());

                        if (!firstRow)
                        {
                            for (int i = 0; i != count; ++i)
                            {
                                if (storeFile)
                                {
                                    store.AddLast(new StoredElement(profile, type, len, delivery, 0));
                                }
                                else
                                {
                                    commission.AddLast(new CommissionElement(profile, type, len, listing));
                                }
                            }
                        }

                        firstRow = false;
                    }
                }

                if (storeFile)
                {
                    foreach (StoredElement e in store)
                    {
                        if (!storedElements.ContainsKey(e.GetProfile()))
                        {
                            storedElements.Add(e.GetProfile(), new List<StoredElement>());
                        }
                        e.SetStoreId(storedElements[e.GetProfile()].Count);
                        storedElements[e.GetProfile()].Add(e);
                    }
                }
                else
                {
                    foreach (CommissionElement e in commission)
                    {
                        if (!commissionElements.ContainsKey(e.GetProfile()))
                        {
                            commissionElements.Add(e.GetProfile(), new List<CommissionElement>());
                        }
                        commissionElements[e.GetProfile()].Add(e);
                    }
                }
            }
        }

        private static string GetCellValue(WorksheetPart worksheetPart, WorkbookPart workbookPart,
            string headerAddress)
        {
            string value = null;

            Cell theCell = worksheetPart.Worksheet.Descendants<Cell>().
              Where(x => x.CellReference == headerAddress).FirstOrDefault();

            if (theCell != null)
            {
                value = theCell.InnerText;

                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            var stringTable =
                                workbookPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            break;

                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }
            }

            return value;
        }
    }
}


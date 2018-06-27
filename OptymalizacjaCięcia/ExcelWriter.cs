using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace OptymalizacjaCięcia
{
    // Służy do aktualizacji pliku z magazynem. Dodaje nowy arkusz, 
    // w którym elementy z magazynu mają zaktualizowaną długość
    class ExcelWriter
    {
        static int rowNumber = 2;

        public static void UpdateStore(string storeDoc)
        {
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(storeDoc, true))
            {
                InsertWorksheet(spreadSheet);

                WorkbookPart workbookPart = spreadSheet.WorkbookPart;

                AddColumnHeaders(workbookPart);
            }
        }

        // Ponieważ algorytm generuje listę rozkrojową osobno dla każdego profilu, 
        // Elementy również są aktualizowane profilami.
        public static void writeProfileToStoreFile(string storeFile, List<StoredElement> store)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(storeFile, true))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

                for (int i = 0; i != store.Count();)
                {
                    int count = 0;
                    int curr_len = store[i].GetLength();
                    StoredElement e = store[i];

                    while (i != store.Count() && store[i].GetLength() == curr_len)
                    {
                        ++i;
                        ++count;
                    }
                    // jeśli zostaje mniej niż 700mm długości, to element nie wraca do magazynu,
                    // tylko idzie na złom
                    if (e.GetDelivery() != "" && e.GetLength() >= 700)
                    {
                        writeElementToStoreFile(workbookPart, e, count);
                    }
                }
            }
        }

        private static void writeElementToStoreFile(WorkbookPart workbook, StoredElement e, int count)
        {
            InsertCell(workbook, "A", rowNumber, e.GetProfile());
            InsertCell(workbook, "B", rowNumber, e.GetElementType());
            InsertCell(workbook, "C", rowNumber, e.GetLength().ToString());
            InsertCell(workbook, "D", rowNumber, count.ToString());
            InsertCell(workbook, "E", rowNumber, e.GetDelivery());
            ++rowNumber;
        }

        private static void AddColumnHeaders(WorkbookPart workbookPart)
        {
            string[] headers = { "Profil", "Gatunek", "Długość", "Sztuk", "Nr Dostawy" };
            string[] columnNames = { "A", "B", "C", "D", "E" };

            for (int i = 0; i != headers.Count(); ++i)
            {
                InsertCell(workbookPart, columnNames[i], 1, headers[i]);
            }
        }

        private static void InsertCell(WorkbookPart workbookPart, string column, int row, string text)
        {
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.Last();

            SharedStringTablePart shareStringPart;
            if (workbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
            {
                shareStringPart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
            }
            else
            {
                shareStringPart = workbookPart.AddNewPart<SharedStringTablePart>();
            }

            int index = InsertSharedStringItem(text, shareStringPart);

            Cell cell = InsertCellInWorksheet(column, (uint)row, worksheetPart);

            cell.CellValue = new CellValue(index.ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
        }

        private static void InsertWorksheet(SpreadsheetDocument spreadSheet)
        {
            WorksheetPart newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());

            Sheets sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            string sheetName = "Sheet" + sheetId;

            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
        }

        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }
    }
}

using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Project_66_bit.Models;
using Project_66_bit.Models.Extensions;

namespace Project_66_bit.Services.ReportService
{
    public class ExcelConverter : IProjectConverter
    {
        public byte[] FormDocument(Project project)
        {
            ExcelWorksheet mainSheet, problemSheet;
            var package = CreatePackage(out mainSheet, out problemSheet);

            PutProjectInfo(mainSheet, project);
            PutCustomerInfo(mainSheet, project.Customer);
            PutModules(mainSheet, project.Modules);
            StylizeFirstPage(mainSheet, project);

            PutProblems(problemSheet, project.Modules);
            StylizeSecondPage(problemSheet, project);

            return package.GetAsByteArray();
        }

        private static ExcelPackage CreatePackage(out ExcelWorksheet mainSheet, out ExcelWorksheet problemSheet)
        {
            var package = new ExcelPackage();
            mainSheet = package.Workbook.Worksheets.Add("Информация о проекте");
            problemSheet = package.Workbook.Worksheets.Add("Задачи");

            return package;
        }

#region PutInfo
        private void PutProjectInfo(ExcelWorksheet mainSheet, Project project)
        {
            mainSheet.Cells[2, 2].Value = "Информация о проекте";

            mainSheet.Cells[3, 2].Value = "Название проекта:";
            mainSheet.Cells[4, 2].Value = "Тип:";
            mainSheet.Cells[5, 2].Value = "Стоимость часа:";
            mainSheet.Cells[6, 2].Value = "Дата начала:";
            mainSheet.Cells[7, 2].Value = "Дата окончания:";
            mainSheet.Cells[8, 2].Value = "Статус:";

            mainSheet.Cells[3, 3].Value = project.Name;
            mainSheet.Cells[4, 3].Value = project.Type;
            mainSheet.Cells[5, 3].Value = project.Cost;
            mainSheet.Cells[6, 3].Style.Numberformat.Format = "dd.mm.yy";
            mainSheet.Cells[6, 3].Formula = $"=DATE({project.StartDate.Year},{project.StartDate.Month},{project.StartDate.Day})";
            mainSheet.Cells[7, 3].Style.Numberformat.Format = "dd.mm.yy";
            mainSheet.Cells[7, 3].Formula = $"=DATE({project.EndDate.Year},{project.EndDate.Month},{project.EndDate.Day})";
            mainSheet.Cells[8, 3].Value = project.Status.DisplayName();
        }

        private void PutCustomerInfo(ExcelWorksheet mainSheet, Customer customer)
        {
            mainSheet.Cells[11, 2].Value = "Информация о заказчике";

            mainSheet.Cells[12, 2].Value = "ФИО:";
            mainSheet.Cells[13, 2].Value = "Номер телефона:";
            mainSheet.Cells[14, 2].Value = "Email:";

            mainSheet.Cells[12, 3].Value = customer.Name;
            mainSheet.Cells[13, 3].Value = customer.PhoneNumber;
            mainSheet.Cells[14, 3].Value = customer.Email;
        }

        private void PutModules(ExcelWorksheet mainSheet, List<Module> modules)
        {
            mainSheet.Cells[2, 7].Value = "Модули";

            mainSheet.Cells[3, 7].Value = "Название";
            mainSheet.Cells[3, 8].Value = "Часы";
            mainSheet.Cells[3, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            mainSheet.Cells[3, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            var totalHours = 0;
            for (var i = 0; i < modules.Count; i++)
            {
                mainSheet.Cells[i + 4, 7].Value = modules[i].Name;
                mainSheet.Cells[i + 4, 8].Value = modules[i].Hours;

                // Cell stylization
                mainSheet.Cells[i + 4, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                mainSheet.Cells[i + 4, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                totalHours += modules[i].Hours;
            }

            mainSheet.Cells[modules.Count + 4, 7].Value = "Всего:";
            mainSheet.Cells[modules.Count + 4, 7].Style.Font.Italic = true;
            mainSheet.Cells[modules.Count + 4, 7].Style.Font.Bold = true;
            mainSheet.Cells[modules.Count + 4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            mainSheet.Cells[modules.Count + 4, 8].Value = totalHours;
            mainSheet.Cells[modules.Count + 4, 8].Style.Font.Bold = true;
            mainSheet.Cells[modules.Count + 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            mainSheet.Cells[modules.Count + 4, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);

        }

        private void PutProblems(ExcelWorksheet problemSheet, List<Module> modules)
        {
            problemSheet.Cells[2, 2].Value = "Задачи";

            problemSheet.Cells[3, 2].Value = "Модуль";
            problemSheet.Cells[3, 3].Value = "Задача";
            problemSheet.Cells[3, 4].Value = "Часы";
            problemSheet.Cells[3, 5].Value = "Начало";
            problemSheet.Cells[3, 6].Value = "Окончание";

            var totalHours = 0;
            var j = 4;
            var startModule = j;
            foreach (var module in modules)
            {
                problemSheet.Cells[j, 2].Value = module.Name;
                problemSheet.Cells[j, 2].Style.Font.Bold = true;

                for (var i = 0; i < module.Problems.Count; i++)
                {
                    problemSheet.Cells[j, 3].Value = module.Problems[i].Name;
                    problemSheet.Cells[j, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    problemSheet.Cells[j, 4].Value = module.Problems[i].Hours;
                    problemSheet.Cells[j, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    problemSheet.Cells[j, 5].Style.Numberformat.Format = "dd.mm.yy";
                    problemSheet.Cells[j, 5].Formula = $"=DATE({module.Problems[i].StartDate.Year},{module.Problems[i].StartDate.Month},{module.Problems[i].StartDate.Day})";
                    problemSheet.Cells[j, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    problemSheet.Cells[j, 6].Style.Numberformat.Format = "dd.mm.yy";
                    problemSheet.Cells[j, 6].Formula = $"=DATE({module.Problems[i].EndDate.Year},{module.Problems[i].EndDate.Month},{module.Problems[i].EndDate.Day})";
                    problemSheet.Cells[j, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    totalHours += module.Problems[i].Hours;
                    j++;
                }

                problemSheet.Cells[startModule, 2, j - 1, 2].Merge = true;
                problemSheet.Cells[startModule, 2, j - 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                problemSheet.Cells[startModule, 2, j - 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                problemSheet.Cells[startModule, 2, j - 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                startModule = j;
            }

            problemSheet.Cells[j, 3].Value = "Всего часов:";
            problemSheet.Cells[j, 3].Style.Font.Italic = true;
            problemSheet.Cells[j, 3].Style.Font.Bold = true;
            problemSheet.Cells[j, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            problemSheet.Cells[j, 4].Value = totalHours;
            problemSheet.Cells[j, 4].Style.Font.Bold = true;

            problemSheet.Cells[1, 1, j, 6].AutoFitColumns();
        }

        #endregion

#region Stylization
        private void StylizeFirstPage(ExcelWorksheet mainSheet, Project project)
        {
            StylizeProjectInfo(mainSheet);
            StylizeCustomerInfo(mainSheet);
            StylizeModuleInfo(mainSheet, project.Modules);

            var endRow = project.Modules.Count + 4 > 8 ? project.Modules.Count + 4 : 14;
            mainSheet.Cells[1, 1, endRow, 8].AutoFitColumns();
        }

        private void StylizeSecondPage(ExcelWorksheet problemSheet, Project project)
        {
            StylizeTitle(problemSheet, 2, 2, 6);
            StylizeProblemSheetHeader(problemSheet);
        }

        private void StylizeProjectInfo(ExcelWorksheet mainSheet)
        {
            StylizeTitle(mainSheet, 2, 2, 3);
            mainSheet.Cells[3, 2, 8, 2].Style.Font.Bold = true;
            mainSheet.Cells[3, 2, 8, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            mainSheet.Cells[3, 3, 8, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        }

        private void StylizeCustomerInfo(ExcelWorksheet mainSheet)
        {
            StylizeTitle(mainSheet, 11, 2, 3);
            mainSheet.Cells[12, 2, 14, 2].Style.Font.Bold = true;
            mainSheet.Cells[12, 2, 14, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            mainSheet.Cells[12, 3, 14, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        }

        private void StylizeModuleInfo(ExcelWorksheet mainSheet, List<Module> modules)
        {
            StylizeTitle(mainSheet, 2, 7, 8);
            mainSheet.Cells[3, 7, 3, 8].Style.Font.Bold = true;
            mainSheet.Cells[3, 7, 3, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private static void StylizeProblemSheetHeader(ExcelWorksheet problemSheet)
        {
            problemSheet.Cells[3, 2].Style.Font.Bold = true;
            problemSheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            problemSheet.Cells[3, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            problemSheet.Cells[3, 3].Style.Font.Bold = true;
            problemSheet.Cells[3, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            problemSheet.Cells[3, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            problemSheet.Cells[3, 4].Style.Font.Bold = true;
            problemSheet.Cells[3, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            problemSheet.Cells[3, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            problemSheet.Cells[3, 5].Style.Font.Bold = true;
            problemSheet.Cells[3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            problemSheet.Cells[3, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            problemSheet.Cells[3, 6].Style.Font.Bold = true;
            problemSheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            problemSheet.Cells[3, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }

        private void StylizeTitle(ExcelWorksheet sheet, int row, int firstColumn, int secondColumn)
        {
            sheet.Cells[row, firstColumn, row, secondColumn].Merge = true;
            sheet.Cells[row, firstColumn].Style.Font.Bold = true;
            sheet.Cells[row, firstColumn].Style.Font.Size = 14;
            sheet.Cells[row, firstColumn].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sheet.Cells[row, firstColumn, row, secondColumn].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        }
#endregion
    }
}

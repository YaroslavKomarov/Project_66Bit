using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using Project_66_bit.Models;

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
            PutProblems(problemSheet, project.Modules);

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
            mainSheet.Cells[6, 3].Value = project.StartDate;
            mainSheet.Cells[7, 3].Value = project.EndDate;
            mainSheet.Cells[8, 3].Value = project.Status;
        }

        private void PutCustomerInfo(ExcelWorksheet mainSheet, Customer customer)
        {
            mainSheet.Cells[11, 2].Value = "Информация о заказчике";

            mainSheet.Cells[12, 2].Value = "ФИО:";
            mainSheet.Cells[13, 2].Value = "Номер телефона";
            mainSheet.Cells[14, 2].Value = "Email";

            mainSheet.Cells[12, 3].Value = customer.Name;
            mainSheet.Cells[13, 3].Value = customer.PhoneNumber;
            mainSheet.Cells[14, 3].Value = customer.Email;
        }

        private void PutModules(ExcelWorksheet mainSheet, List<Module> modules)
        {
            mainSheet.Cells[2, 5].Value = "Модули";
            mainSheet.Cells[3, 5].Value = "Название";
            mainSheet.Cells[3, 6].Value = "Часы";

            var hoursTotal = 0;
            for (var i = 0; i < modules.Count; i++)
            {
                mainSheet.Cells[i + 4, 5].Value = modules[i].Name;
                mainSheet.Cells[i + 4, 6].Value = modules[i].Hours;

                hoursTotal += modules[i].Hours;
            }

            mainSheet.Cells[modules.Count + 4, 5].Value = "Всего";
            mainSheet.Cells[modules.Count + 4, 6].Value = hoursTotal;
        }

        private void PutProblems(ExcelWorksheet problemSheet, List<Module> modules)
        {
            problemSheet.Cells[2, 2].Value = "Задачи";
            problemSheet.Cells[3, 2].Value = "Модуль";
            problemSheet.Cells[3, 3].Value = "Задача";
            problemSheet.Cells[3, 4].Value = "Часы";
            problemSheet.Cells[3, 5].Value = "Начало";
            problemSheet.Cells[3, 6].Value = "Окончание";
        }
#endregion
    }
}
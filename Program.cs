using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelXMLControl
{
    class Program
    {
        static IEnumerable GetColors()
        {
            yield return XLColor.Red;
            yield return XLColor.Amber;
            yield return XLColor.AppleGreen;
            yield return XLColor.AtomicTangerine;
            yield return XLColor.BallBlue;
            yield return XLColor.Bittersweet;
            yield return XLColor.CalPolyPomonaGreen;
            yield return XLColor.CosmicLatte;
            yield return XLColor.DimGray;
            yield return XLColor.ZinnwalditeBrown;
        }

        static void Main(string[] args)
        {
            using (var workbook = new XLWorkbook())
            {
                //Generamos la hoja
                var worksheet = workbook.Worksheets.Add("FixedBuffer");
                //Generamos la cabecera
                //Para GIt
                worksheet.Cell("A1").Value = "Nombre";
                worksheet.Cell("B1").Value = "Color";

                //-----------Le damos el formato a la cabecera----------------
                var rango = worksheet.Range("A1:B1"); //Seleccionamos un rango
                rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
                rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
                rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
                rango.Style.Fill.BackgroundColor = XLColor.AliceBlue; //Indicamos el color de background


                //-----------Genero la tabla de colores-----------
                int nRow = 2;
                foreach (var color in GetColors())
                {
                    worksheet.Cell(nRow, 1).Value = color.ToString(); //Indicamos el valor en la celda nRow, 1
                    worksheet.Cell(nRow, 2).Style.Fill.BackgroundColor = (XLColor)color; //Cambiamos el color de background de la celda nRow,2
                    nRow++;
                }

                //Aplico los formatos
                rango = worksheet.Range(2, 1, nRow - 1, 2); //Seleccionamos un rango
                rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
                rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
                rango.Style.Font.SetFontName("Courier New"); //Utilizo una fuente monoespacio
                rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; //Alineamos horizontalmente
                rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente


                worksheet.Columns(1, 2).AdjustToContents(); //Ajustamos el ancho de las columnas para que se muestren todos los contenidos

                workbook.SaveAs("CellFormating.xlsx");  //Guardamos el fichero
            }
        }
    }
}

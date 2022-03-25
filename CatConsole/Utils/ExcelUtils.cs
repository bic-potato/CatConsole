using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CatConsole.Utils
{
    public class ExcelUtils
    {
        private String path;

        public ExcelUtils(String file)
        {
            path = file;
        }
        public List<Cat> getCatList()
        {
            var package = new ExcelPackage(new FileInfo(path));

                List<Cat> result = new List<Cat>();
                var ws = package.Workbook.Worksheets[0];
                int minRowNum = 13; //工作区开始行号
                int maxRowNum = ws.Dimension.End.Row; //工作区结束行号
            for (int i = minRowNum; i < maxRowNum; i++)
            {
                Console.WriteLine($"读取到数据：{ws.Cells[i, 3].Value}");
                var cat = new Cat(Convert.ToInt32(ws.Cells[i, 2].Value), ws.Cells[i, 3].Value == null ? null : ws.Cells[i, 3].Value.ToString(), Convert.ToInt32(ws.Cells[i, 4].Value), ws.Cells[i, 5].Value==null ? null : ws.Cells[i, 5].Value.ToString(), Convert.ToInt32(ws.Cells[i, 7].Value),
                    ws.Cells[i, 8].Value==null ? null : ws.Cells[i, 8].Value.ToString(), Convert.ToInt32(ws.Cells[i, 9].Value), ws.Cells[i, 10].Value == null ? null : ws.Cells[i, 10].Value.ToString(), Convert.ToInt32(ws.Cells[i, 11].Value), ws.Cells[i, 12].Value==null ? null : Convert.ToDateTime(ws.Cells[i, 12].Value.ToString().Replace("年", "/").Replace("月", "/").Replace("号", "")), ws.Cells[i, 13].Value==null ? null : Convert.ToDateTime(ws.Cells[i, 13].Value), ws.Cells[i, 14].Value == null ? null : ws.Cells[i, 14].Value.ToString(), ws.Cells[i, 15].Value == null ? null : Convert.ToInt32(ws.Cells[i, 15].Value), ws.Cells[i, 16].Value == null ? null : ws.Cells[i, 16].Value.ToString(), ws.Cells[i, 17].Value == null ? null : ws.Cells[i, 17].Value.ToString(), ws.Cells[i, 18].Value==null ? null : ws.Cells[i, 18].Value.ToString(), ws.Cells[i, 19].Value == null ? null : ws.Cells[i, 19].Value.ToString(), route: ws.Cells[i, 20].Value == null ? null : ws.Cells[i, 20].Value.ToString(), ws.Cells[i, 21].Value == null ? null : Convert.ToDateTime(ws.Cells[i, 21].Value), ws.Cells[i, 22].Value == null ? null : Convert.ToDateTime(ws.Cells[i, 22].Value), ws.Cells[i, 23].Value == null ? null : ws.Cells[i, 23].Value.ToString(), ws.Cells[i, 24].Value == null ? null : Convert.ToInt32(ws.Cells[i, 24].Value), ws.Cells[i, 25].Value == null ? null : Convert.ToInt32(ws.Cells[i, 25].Value));
                result.Add(cat);
                

            }
            Console.WriteLine();
            return result;

        }
        
    }


}

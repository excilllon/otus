using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseReader
{
    public class ArrayTestCase
    {
        /// <summary>
        /// Входящее значение
        /// </summary>
        public int[] In { get; set; }
        /// <summary>
        /// Ожидаемы результат
        /// </summary>
        public int[] Expected { get; set; }
        /// <summary>
        /// Номер тестового кейса
        /// </summary>
        public string TestCaseNumber { get; set; }
        /// <summary>
        /// раздел
        /// </summary>
        public string TestCaseType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseReader
{
    public class TestCase
    {
        /// <summary>
        /// Входящее значение
        /// </summary>
        public string In { get; set; }
        /// <summary>
        /// Ожидаемы результат
        /// </summary>
        public string Expected { get; set; }
        /// <summary>
        /// Номер тестового кейса
        /// </summary>
        public string TestCaseNumber { get; set; }
    }
}

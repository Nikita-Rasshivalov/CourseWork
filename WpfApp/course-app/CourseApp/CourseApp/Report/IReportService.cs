using NpgsqlTypes;
using System.Windows.Controls;

namespace CourseApp.Reports
{
    /// <summary>
    /// Интерфейс для получения отчетов
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получить информацию по конкретному складу
        /// </summary>
        /// <param name="stockId">id склада</param>
        /// <param name="reportComboBox">элемент комбобокса</param>
        void GetReportS(int stockId, ComboBox reportComboBox);
        /// <summary>
        /// Получить информацию обо всех складах
        /// </summary>
        void GetReportAS();
        /// <summary>
        /// Получить прибыль по складам
        /// </summary>
        /// <param name="dataFrom">дата с какой брать данные</param>
        /// <param name="dataTo">дата по какую брать данные</param>
        void GetReportDS(NpgsqlDate dataFrom, NpgsqlDate dataTo);
        /// <summary>
        /// Получение доходных товаров
        /// </summary>
        void GetReportD();

    }
}

namespace Project.Reports
{
    public interface IReportGenerator<TEntity, TReportEntity>
    {
        public Report<TReportEntity> GenerateReport(TEntity entity);
    }
}

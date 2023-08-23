namespace RentalCompany.SharedKernel.Operation_Result
{
    public class OperationResult<T> where T : class
    {
        public T Reslut { get; set; }
        public IEnumerable<T> RangeResults { get; set; }
        public string Message { get; set; }
        public OperationResultTypes OperationResultType { get; set; }

        public Exception Exception { get; set; }
        public int? StatusCode { get; set; }
        public bool IsSuccess => OperationResultType == OperationResultTypes.Success;

        public bool HasException => this.OperationResultType == OperationResultTypes.Exception;

    }
}
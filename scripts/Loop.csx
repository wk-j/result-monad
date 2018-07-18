class Result {
    public bool Success { set;get;} = true;
}

Result CreateResult() => new Result { Success = true };

var result = new Result();
while(result.Success) {
    Console.WriteLine("Gogo");
    System.Threading.Thread.Sleep(100);
    result = CreateResult();
}


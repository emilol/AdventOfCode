<Query Kind="Program" />

void Main()
{
	IsMatch(111111).Dump();
	IsMatch(223450).Dump();
	IsMatch(123789).Dump();
	
	Run().Dump("possible passwords");
}

int Run()
{
	return Enumerable.Range(402328, 864247 - 402328)
		.Where(IsMatch)
		.Count();
}

bool IsMatch(int password) {
	var digits = ToDigits(password);
	return IsOrdered(digits) && HasDouble(digits);
}

bool IsOrdered(IEnumerable<int> digits)
{
	var previous = -1;
	foreach (var digit in digits)
	{
		if (previous > digit)
		{
			previous = digit;
			return false;
		}
		previous = digit;
	}

	return true;
}

bool HasDouble(IEnumerable<int> digits)
{
	return digits.GroupBy(d => d).Any(d => d.Count() >= 2);
}

IEnumerable<int> ToDigits(int password) {
	return password.ToString().Select(d => (int)Char.GetNumericValue(d));
}
<Query Kind="Program" />

void Main()
{
	var trials = (int)Math.Pow(4,10);
	foreach(var i in Enumerable.Range(0, trials))
	{
		var answer = GetQuaternary(i);
		if(IsValid(answer))
		{
			answer.Dump();
		}
	}
}

char[] GetQuaternary(int decimalNumber)
{
	var answer = new char[10];
	foreach(var i in Enumerable.Range(0, 10))
	{
		var remainder = decimalNumber % 4;
		decimalNumber = decimalNumber / 4;
		answer[i] = Convert.ToChar(65+remainder);
	}
	
	return answer;
}

bool IsValid(char[] answer)
{
	// Q2
	if(
		!(
			(answer[1]=='A' && answer[4] == 'C') ||
			(answer[1]=='B' && answer[4] == 'D') ||
			(answer[1]=='C' && answer[4] == 'A') ||
			(answer[1]=='D' && answer[4] == 'B')
		)
	)
	{
		return false;
	}
	
	//Q3
	if(
		!(
			(answer[2]=='A' && answer[2]!=answer[5] && answer[2]!=answer[1] && answer[2]!=answer[3]) ||
			(answer[2]=='B' && answer[5]!=answer[2] && answer[5]!=answer[1] && answer[5]!=answer[3]) ||
			(answer[2]=='C' && answer[1]!=answer[2] && answer[1]!=answer[5] && answer[1]!=answer[3]) ||
			(answer[2]=='D' && answer[3]!=answer[2] && answer[3]!=answer[5] && answer[3]!=answer[1])
		)
	)
	{
		return false;
	}
	
	//Q4
	if(
		!(
			(answer[3]=='A' && answer[0]==answer[4]) ||
			(answer[3]=='B' && answer[1]==answer[6]) ||
			(answer[3]=='C' && answer[0]==answer[8]) ||
			(answer[3]=='D' && answer[5]==answer[9])
		)
	)
	{
		return false;
	}
	
	//Q5
	if(
		!(
			(answer[4]=='A' && answer[4]==answer[7]) ||
			(answer[4]=='B' && answer[4]==answer[3]) ||
			(answer[4]=='C' && answer[4]==answer[8]) ||
			(answer[4]=='D' && answer[4]==answer[6])
		)
	)
	{
		return false;
	}
	
	//Q6
	if(
		!(
			(answer[5]=='A' && answer[7]==answer[1] && answer[7]==answer[3]) ||
			(answer[5]=='B' && answer[7]==answer[0] && answer[7]==answer[5]) ||
			(answer[5]=='C' && answer[7]==answer[2] && answer[7]==answer[9]) ||
			(answer[5]=='D' && answer[7]==answer[4] && answer[7]==answer[8])
		)
	)
	{
		return false;
	}

	//Q7
	if(
		!(
			(answer[6]=='A' && GetSelectionCount(answer).First().Key=='C') ||
			(answer[6]=='B' && GetSelectionCount(answer).First().Key=='B') ||
			(answer[6]=='C' && GetSelectionCount(answer).First().Key=='A') ||
			(answer[6]=='D' && GetSelectionCount(answer).First().Key=='D')
		)
	)
	{
		return false;
	}
	
	//Q8
	if(
		!(
			(answer[7]=='A' && IsAdjacent(answer[6], answer[1])) ||
			(answer[7]=='B' && IsAdjacent(answer[4], answer[1])) ||
			(answer[7]=='C' && IsAdjacent(answer[1], answer[1])) ||
			(answer[7]=='D' && IsAdjacent(answer[9], answer[1]))
		)
	)
	{
		return false;
	}
	
	//Q9
	var isAns1And6Same = answer[0] == answer[5];
	
	if(
		!(
			(answer[8]=='A' && isAns1And6Same != (answer[5] == answer[4])) ||
			(answer[8]=='B' && isAns1And6Same != (answer[9] == answer[4])) ||
			(answer[8]=='C' && isAns1And6Same != (answer[1] == answer[4])) ||
			(answer[8]=='D' && isAns1And6Same != (answer[8] == answer[4]))
		)
	)
	{
		return false;
	}
	
	//Q10
	if(
		!(
			(answer[9]=='A' && GetSelectionCount(answer).Last().Value - GetSelectionCount(answer).First().Value == 3) ||
			(answer[9]=='B' && GetSelectionCount(answer).Last().Value - GetSelectionCount(answer).First().Value == 2) ||
			(answer[9]=='C' && GetSelectionCount(answer).Last().Value - GetSelectionCount(answer).First().Value == 4) ||
			(answer[9]=='D' && GetSelectionCount(answer).Last().Value - GetSelectionCount(answer).First().Value == 1)
		)
	)
	{
		return false;
	}
	
	return true;
}

IOrderedEnumerable<KeyValuePair<char, int>> GetSelectionCount(char[] answer)
{
	var count = new Dictionary<char, int>()
	{
		{'A', 0},
		{'B', 0},
		{'C', 0},
		{'D', 0},
	};
	
	foreach(var ans in answer)
	{
		count[ans]++;
	}
	
	return count.OrderBy(x => x.Value);
}

bool IsAdjacent(char first, char second)
{
	if((int)first - (int)second == 1 || (int)first - (int)second == -1)
	{
		return true;
	}
	
	return false;
}
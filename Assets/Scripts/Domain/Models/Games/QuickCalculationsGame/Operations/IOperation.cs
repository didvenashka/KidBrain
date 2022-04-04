public interface IOperation
{
    int Operate(int firstNumber, int secondNumber);
    (int, int) GenerateNumbers(Difficulty difficulty);

    string Symbol();
}

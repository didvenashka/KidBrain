using System.Collections.Generic;

public interface IQuickEyeGameManager
{
    QuickEyeGame CreateNewGame();
    IEnumerable<int> GetNewVariants(int numberOfVariants, int mainPictureId);
}

using System.Collections.Generic;

public interface IReactMatchGameManager
{
    ReactMatchGame CreateNewGame();
    IEnumerable<int> GetNewVariants(int numberOfVariants, int mainPictureId);
}

using System.Collections.Generic;

public interface IQuickEyeGameManager : IGameManager<QuickEyeGame>
{
    IEnumerable<int> GetNewVariants(int numberOfVariants, int mainPictureId);
    //byte[] GetPicture(int pictureId);
}

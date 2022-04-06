using System;
using System.Collections.Generic;
using System.Linq;

public class QuickEyeGameManager : IQuickEyeGameManager
{
    private readonly Random _random = new Random();
    //private readonly IPicturesRepository _picturesRepository;
    private const int _numberOfPictures = 8; //TODO get number from repository (+ create PicturesRepository)
    private const int _numberOfMatches = 10;
    private const int _numberOfVariants = 4;
    private const int _durationOfAllVariantsInSeconds = 2;
    private const int _reward = 10;
    //TODO add rotation (0, 90, 180, 270) + pictureId -> PictureModel + pictureCollectionId

    public QuickEyeGameManager(/*IPicturesRepository picturesRepository*/)
    {
        //_picturesRepository = picturesRepository;
    }

    public QuickEyeGame CreateNewGame()
    {
        var matches = CreateMatches(_numberOfMatches);

        return new QuickEyeGame
        {
            Matches = matches
        };
    }

    public IEnumerable<int> GetNewVariants(int numberOfVariants, int mainPictureId)
    {
        var variantsIds = Enumerable
            .Range(0, _numberOfPictures)
            .OrderBy(x => Guid.NewGuid())
            .Take(numberOfVariants)
            .ToList();

        if (!variantsIds.Contains(mainPictureId))
        {
            variantsIds[_random.Next(numberOfVariants)] = mainPictureId;
        }

        return variantsIds;
    }

    private IEnumerable<Match> CreateMatches(int numberOfMatches)
    {
        var matches = new List<Match>();

        for (int i = 0; i < numberOfMatches; i++)
        {
            var match = CreateMatch(_numberOfVariants, _durationOfAllVariantsInSeconds);
            matches.Add(match);
        }

        return matches;
    }

    private Match CreateMatch(int numberOfVariants, int durationOfAllVariantsInSeconds)
    {
        var mainPictureId = _random.Next(_numberOfPictures);

        var variantsIds = GetNewVariants(numberOfVariants, mainPictureId);

        return new Match
        {
            MainPictureId = mainPictureId,
            VariantsIds = variantsIds,
            DurationOfAllVariantsInSeconds = durationOfAllVariantsInSeconds,
            Reward = _reward
        };
    }
}

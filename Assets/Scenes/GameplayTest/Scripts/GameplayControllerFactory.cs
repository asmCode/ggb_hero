public class GameplayControllerFactory
{
    public static GameplayController Create(Gameplay gameplay)
    {
        return new InfiniteGameplayController(gameplay);
    }
}

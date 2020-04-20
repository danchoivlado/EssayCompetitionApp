namespace EssayCompetition.Services.Data.GradeServices
{
    public interface IGradeService
    {
        T GetGradeDetails<T>(int essayId);
    }
}

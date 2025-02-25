using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
            List<QuizItem> quizItems = new List<QuizItem>();
            
            quizItems.Add(quizItemRepo.Add(new QuizItem(id: 1, correctAnswer: "8", question: "4 + 4",
                incorrectAnswers: new List<string>() { "6", "7", "9" })));

            quizItems.Add(quizItemRepo.Add(new QuizItem(id: 2, correctAnswer: "9", question: "3 * 3",
                incorrectAnswers: new List<string>() { "6", "8", "10" })));
            
            quizItems.Add(quizItemRepo.Add(new QuizItem(id: 3, correctAnswer: "2", question: "10 / 5",
                incorrectAnswers: new List<string>() { "3", "4", "5" })));

            quizRepo.Add(new Quiz(id: 1, items: quizItems, title: "Quiz Matematyczny"));
        }
    }
}
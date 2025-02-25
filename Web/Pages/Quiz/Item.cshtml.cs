using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BackendLab01.Pages.Quiz
{
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;
        private readonly ILogger<QuizModel> _logger;

        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public string Question { get; set; } = string.Empty;

        [BindProperty]
        public List<string> Answers { get; set; } = new();

        [BindProperty]
        public string UserAnswer { get; set; } = string.Empty;

        [BindProperty]
        public int QuizId { get; set; }

        [BindProperty]
        public int ItemId { get; set; }

        [BindProperty]
        public int? NextItemIndex { get; set; }

        public IActionResult OnGet(int quizId, int itemIndex)
        {
            QuizId = quizId;
            var quiz = _userService.FindQuizById(quizId);
            if (quiz == null || quiz.Items == null || quiz.Items.Count == 0)
            {
                _logger.LogWarning("Quiz not found or empty. QuizId: {QuizId}", quizId);
                return RedirectToPage("/Error");
            }

            if (itemIndex >= quiz.Items.Count)
            {
                return RedirectToPage("/Error");
            }

            var quizItem = quiz.Items[itemIndex];
            ItemId = quizItem.Id;
            NextItemIndex = itemIndex + 1 < quiz.Items.Count ? itemIndex + 1 : null;

            Question = quizItem.Question;
            Answers = new List<string> { quizItem.CorrectAnswer };

            if (quizItem.IncorrectAnswers != null)
            {
                Answers.AddRange(quizItem.IncorrectAnswers);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            _userService.SaveUserAnswerForQuiz(QuizId, 1, ItemId, UserAnswer);

            if (NextItemIndex == null)
            {
                return RedirectToPage("/Quiz/Summary", new { quizId = QuizId, userId = 1 });
            }

            return RedirectToPage("/Quiz/Item", new { quizId = QuizId, itemIndex = NextItemIndex });
        }
    }
}

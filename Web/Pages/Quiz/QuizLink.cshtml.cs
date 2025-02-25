using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BackendLab01.Pages.Quiz
{
    public class QuizLinkModel : PageModel
    {
        private readonly IQuizUserService _userService;

        public QuizLinkModel(IQuizUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public List<BackendLab01.Quiz> Quizzes { get; private set; } = new();

        public void OnGet()
        {
            Quizzes = _userService.GetAllQuizzes();
        }
    }

}
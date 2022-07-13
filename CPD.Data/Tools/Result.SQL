select 'Module' = a.Naam, 'Article' = b.Naam, c.CorrectAnswer, c.NumberWithinArticle, c.Question, b.ArticleId, c.QuestionId 
from Module as a inner join Article as b on a.ModuleId = b.ModuleId
inner join Question as c on b.ArticleId = c.ArticleId
where a.SurveyId = 2
order by a.ModuleId, b.ArticleId, c.NumberWithinArticle
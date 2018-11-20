using System.Collections.Generic;
using System.Linq;

namespace QuestCoreNS
{
    /// <summary>
    /// Функционал для Interview
    /// </summary>
    public class InterviewManipulator
    {
        private Interview interview;

        public InterviewManipulator(Interview interview)
        {
            this.interview = interview;
        }

        /// <summary>
        /// Переходим к следующему вопросу
        /// </summary>
        public void GoToNextQuestion()
        {
            if (interview.IsFinished) return;//интервью уже завершено

            //получаем индекс текущего вопроса
            var currentQuestIndex = interview.CurrentAnswer == null ? -1 : interview.questionnaire.FindIndex(q => q.Id == interview.CurrentAnswer.QuestId);

            //ищем следующий вопрос, у которго выполняется условие показа
            for (int i = currentQuestIndex + 1; i < interview.questionnaire.Count; i++)
            {
                //получаем условие отображения вопроса
                var condition = interview.questionnaire[i].Condition;

                //условие выполняется?
                var clalculator = new ConditionCalculator();
                if (clalculator.Calculate(interview.anketa, condition))
                {
                    //нашли
                    var quest = interview.questionnaire[i];

                    //добавляем текущий ответ в список отвеченных вопросов
                    if(interview.CurrentAnswer != null)
                        interview.PassedAnswers.Add(interview.CurrentAnswer);

                    //создаем новый Answer для нового вопроса
                    interview.CurrentAnswer = new Answer { QuestId = quest.Id };
                    interview.anketa.Add(interview.CurrentAnswer);
                    //
                    return;
                }
            }

            //нет следующего вопроса, анкета завершена
            if (interview.CurrentAnswer != null)
            {
                interview.PassedAnswers.Add(interview.CurrentAnswer);
                interview.CurrentAnswer = null;
            }
            interview.IsFinished = true;
        }

        /// <summary>
        /// Получение списка альтренатив, разрешенных к показу, для текущего вопроса
        /// </summary>
        public IEnumerable<Alternative> GetAllowedAlternatives()
        {
            if (interview.IsFinished) yield break;//интервью уже завершено
            if (interview.CurrentAnswer == null) yield break;//нет текущего вопроса

            //получаем текущий вопрос
            var quest = interview.questionnaire.First(q => q.Id == interview.CurrentAnswer.QuestId);

            //перебираем альтернативы, проверяем условние показа, возвращаем те, которые разрешены к показу
            var clalculator = new ConditionCalculator();
            foreach (var alt in quest)
            if (clalculator.Calculate(interview.anketa, alt.Condition))
                yield return alt;
        }

        /// <summary>
        /// Возврат к предыдущему вопросу
        /// </summary>
        public void GoToPrevQuestion()
        {
            var prevAnswer = interview.PassedAnswers.Last();
            interview.anketa.Remove(prevAnswer);
            interview.PassedAnswers.Remove(prevAnswer);
            interview.CurrentAnswer = prevAnswer;
            interview.IsFinished = false;
        }
    }
}
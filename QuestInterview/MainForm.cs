using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuestCoreNS;

namespace QuestInterviewNS
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Нужно сохранить в базу данных?
        /// </summary>
        public bool SaveToDB { get; set; } = true;

        //интервью
        private Interview interview;

        //функционал интервью
        private InterviewManipulator interviewManipulator;

        //текущий опросник
        private Questionnaire questionnaire;

        //текущая анкета
        private Anketa anketa;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Questionnaire questionnaire) : this()
        {
            this.questionnaire = questionnaire;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //создаем новую анкету
            anketa = new Anketa();

            //запрашиваем опросник, если он не задан
            if (questionnaire == null)
            {
                var ofd = new OpenFileDialog() { Filter = "Опросник|*.q", Title = "Выберите опросник" };
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    questionnaire = SaverLoader.Load<Questionnaire>(ofd.FileName);
                }
                else
                {
                    //если пользователь не выбрал опросник - просто завершаем приложение
                    Close();
                    return;
                }
            }

            //создаем процесс опроса (интервью)
            interview = new Interview(questionnaire, anketa);
            //создаем манипулятор для интервью
            interviewManipulator = new InterviewManipulator(interview);

            //переходим к первому вопросу
            interviewManipulator.GoToNextQuestion();

            //строим интерфейс
            Build();
        }

        /// <summary>
        /// Построение интерфейса для опросника
        /// </summary>
        public void Build()
        {
            //создаем хелпер отрисовки, останавливаем отрисовку
            var helper = new ControlHelper(pnAnswers);

            //очищаем панель ответов
            pnAnswers.Controls.Clear();

            //отображаем уже отвеченные вопросы
            foreach (var answer in interview.PassedAnswers)
            {
                //создаем панель ответа
                var pn = new AnswerPanel();
                //строим
                pn.Build(interviewManipulator, questionnaire.First(q => q.Id == answer.QuestId), answer, true);
                //добавляем на форму
                pn.Parent = pnAnswers;
            }

            //отображаем вопрос, на который нужно ответить
            if (interview.CurrentAnswer != null)
            {
                //создаем панель ответа
                var pn = new AnswerPanel();
                //строим
                pn.Build(interviewManipulator, questionnaire.First(q => q.Id == interview.CurrentAnswer.QuestId), interview.CurrentAnswer, false);
                //добавляем на форму
                pn.Parent = pnAnswers;
            }

            //настриваем видимость кнопок
            btBack.Parent = interview.PassedAnswers.Count > 0 ? pnAnswers : null;
            btNext.Parent = interview.IsFinished ? null : pnAnswers;
            btFinish.Parent = interview.IsFinished ? pnAnswers : null;

            //восстанавливаем отрисовку
            helper.ResumeDrawing();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            //переходим к следующему вопросу
            interviewManipulator.GoToNextQuestion();
            //строим интерфейс
            Build();
        }

        private void btFinish_Click(object sender, EventArgs e)
        {
            //предлагаем сохранить анкету
            if (MessageBox.Show("Сохранить анкету?", "Сохранение анкеты", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (SaveToDB)
                {
                    //сохраняем анкету в БД
                    new ExportToDB().Export(questionnaire, anketa);
                }
                else
                {
                    //запрашиваем имя файла
                    var sfd = new SaveFileDialog() {Filter = "Анкета|*.a", FileName = Guid.NewGuid().ToString()};
                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        //сохраняем анкету
                        SaverLoader.Save(anketa, sfd.FileName);
                    }
                }
            }
            //выходим
            Close();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            //переходим к пред вопросу
            interviewManipulator.GoToPrevQuestion();
            //строим интерфейс
            Build();
        }
    }
}

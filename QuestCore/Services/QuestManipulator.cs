using System.Linq;
using System.Windows.Forms;

namespace QuestCore
{
    /// <summary>
    /// ���������� ����������� � ��������
    /// </summary>
    public class QuestManipulator
    {
        /// <summary>
        /// �������� ����� ������������
        /// </summary>
        public Alternative AddNewAlt(Quest quest)
        {
            if (quest.QuestType != QuestType.SingleAnswer)
            {
                MessageBox.Show(@"��� ������� ���� ������� �� ��������� ������������", @"���������� �����������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            //��������� ���������� ��� ������������
            var code = 1;

            while (quest.Any(a => a.Code == code))//����������� �������, ���� �� ������ ����, �������� ��� ���
                code++;

            var alt = new Alternative { Code = code, Title = "������� "  + code };
            quest.Add(alt);

            return alt;
        }

        /// <summary>
        /// ������� ������������
        /// </summary>
        public void RemoveAlt(Quest quest, Alternative alt)
        {
            quest.Remove(alt);
        }

        public void ClearAllAlts(Quest quest)
        {
            quest.Clear();
        }

        /// <summary>
        /// ����������� ������������
        /// </summary>
        public void MoveAlt(Quest quest, Alternative alt, UserPanelActionType dir)
        {
            ListHelper.MoveElement(quest, alt, (int)dir);
        }
    }
}
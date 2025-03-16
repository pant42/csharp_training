using System;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public GroupHelper (ApplicationManager manager) : base (manager) { }

        // Для создания группы
        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialogue();
        }

        // Взять список групп (GetItemCount и GetText)
        public List<GroupData> GetGroupList()
        {
            // Новый пустой список групп, куда поместим все собранные группы
            List<GroupData> list = new List<GroupData>();

            // Открываем диалоговое окно, где у нас список групп
            OpenGroupsDialogue();
            GetGroupsNames(list);

            // Закрываем окно списка групп
            CloseGroupsDialogue();
            return list;
        }

        // Метод который собирает имена групп 
        private void GetGroupsNames(List<GroupData> list)
        {
            // 1. Собираем кол-во элементов
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount",
                "#0",
                "");

            // 2. Для каждого из элементов берем GetText по циклу, от [0] элемента, до элемента i(count) = GetItemCount
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetText",
                "#0|#" + i,
                "");
                // Само добавление имени группы 
                list.Add(new GroupData()
                {
                    Name = item
                });
            }
        }

        internal void RemovingGroup(GroupData removedGroupName)
        {
            // Открываем список групп
            OpenGroupsDialogue();

            // Клик по имени удаляемой группы
            aux.ControlClick(WINTITLE, "", removedGroupName.Name);

            // Клик по "удалить"
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");

            // Подтверждение удаления
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");

            // Нажимаем "ок"
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            
            CloseGroupsDialogue();
        }


        // Вспомогательные методы Открытия и Закрытия окна групп 
        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
        }
        private void CloseGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }


    }

}
using GameSaveBackup.Model;

namespace ScriptExecutor.Controllers
{
    public interface IForm_MainController
    {
        void AddGame(Game game);

        void OnModifyClick(Game game, int index);

        void OnDeleteClick(int index);

        void OnCheck(int index, bool c);
    }
}
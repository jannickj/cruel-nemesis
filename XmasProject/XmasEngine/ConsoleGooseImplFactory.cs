using XmasEngineController;
using XmasEngineController.Console;
using XmasEngineModel;
using XmasEngineView.Console;

namespace XmasEngine
{
    public class ConsoleGooseImplFactory : GooseEngineFactory<GooseConsoleView,GooseController>
    {

        public override GooseConsoleView ConstructView(XmasModel model)
        {
			//TODO: FIX view construction
	        return null; //new GooseConsoleView(model,contructWorldView(model.World));
        }

        private ConsoleWorldView contructWorldView(XmasWorld modelworld)
        {
            return new ConsoleWorldView(modelworld);
        }

        public override GooseController ContructController(XmasModel model, GooseConsoleView view)
        {
            return new ConsoleController(model, view);
        }
    }
}

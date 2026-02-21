using AbsMenu;
using ContractLine;

public class ContractBoardView : AbsMenuView<ContractBoardPresenter, ContractLinePresenter, ContractLineView>
{
    public void Refresh()
    {
        _presenter.GenerateContract();
    }
}

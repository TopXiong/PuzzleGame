using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BranchItem : BaseClickObject
{
    public string Name;

    public override void OnClick()
    {
        GameManager.Instance.EndSuspend(Name);
        Destroy(transform.parent.gameObject, 2f);
        transform.parent.gameObject.SetActive(false);
    }
}

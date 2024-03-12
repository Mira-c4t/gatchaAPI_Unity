using System.Text;

[System.Serializable]
public class Data
{
    public string results;
    public Item[] items;

    public override string ToString()
    {
        var ret = new StringBuilder();
        foreach (var row in items)
        {
            ret.AppendLine("{");
            ret.AppendLine("  reality：" + row.reality);
            ret.AppendLine("  name：" + row.name);
            ret.AppendLine("  imageLINK" + row.imageLINK);
            ret.AppendLine("  DebugValue：" + row.DebugValue);
            ret.AppendLine("},");
        }
        return ret.ToString();
    }
}
[System.Serializable]
public class Item
{
    public int reality;
    public string name;
    public string imageLINK;
    public float DebugValue;
}
[System.Serializable]
public class Results
{
    public int reality;
    public string name;
    public string imageLINK;
    public float DebugValue;
}

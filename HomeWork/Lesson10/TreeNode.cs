namespace Lesson10;

public class TreeNode
{
    public int Key { get; set; }
    public TreeNode? Left { get; set; }
    public TreeNode? Right { get; set; }
    public int Height { get; set; } = 1;
}
using System;

/* 
 * Postanowiłem użyć wersji Visitora znającej strukturę kompozytu.
 * W celu wyznaczenia głębokości drzewa musimy mieć większą świadomość i kontrolę nad
 * naszym położeniem w drzewie. Użycie wizytora nieznającego struktry drzewa nie dostarczyłoby
 * nam odpowiednio dużo informacji.
 * 
 * Możliwe, że dałoby się zaprojektować interfejs dla wizytora, który w jakiś sposób pozwoliłby nam
 * osiągnąć oczekiwany rezultat ale znacznie czytelniejszym wydaje mi się jednak podejście z 
 * wizytorem znającym strukturę.
 */

namespace zadanie_3
{
    public class MainClass
    {
        public static void Main()
        {
            Tree root = new TreeNode(
                new TreeNode( // głębokość 1
                    new TreeNode( // głębokość 2
                        new TreeNode( // głębokość 3
                            new TreeNode( // głębokość 4
                                new TreeNode( // głębokość 5
                                    new TreeNode( // głębokość 6
                                        new TreeNode( // głębokość 7
                                            new TreeLeaf(8), new TreeLeaf(8) // głębokość 8
                                        ),
                                        new TreeLeaf(7)
                                    ),
                                    new TreeLeaf(6)
                                ),
                                new TreeLeaf(5)
                            ),
                            new TreeLeaf(4)
                        ),
                        new TreeLeaf(3)
                    ),
                    new TreeLeaf(2)
                ),
                new TreeLeaf(1)
            );
            DepthTreeVisitor visitor = new DepthTreeVisitor();
            visitor.Visit(root);
            Console.WriteLine("Głębokość drzewa to {0}", visitor.Depth);
            Console.ReadLine();
        }
    }
    
    public abstract class Tree { }
    
    public class TreeNode : Tree {
        public TreeNode(Tree left, Tree right)
        {
            Left = left;
            Right = right;
        }
        public Tree Left { get; set; }
        public Tree Right { get; set; }
    }

    public class TreeLeaf : Tree 
    {
        public TreeLeaf(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }

    public abstract class TreeVisitor {
        public void Visit(Tree tree) {
            if (tree is TreeNode) { 
                this.VisitNode((TreeNode) tree);
            }
            if (tree is TreeLeaf) {
                this.VisitLeaf((TreeLeaf) tree);
            }
        }

        public virtual void VisitNode(TreeNode node) {
            if (node != null)
            {
                this.Visit(node.Left);
                this.Visit(node.Right);
            }
        }
        public virtual void VisitLeaf(TreeLeaf leaf) { }
    }

    /// <summary>
    /// Visitor obliczający głębokość drzew binarnych.
    /// </summary>
    public class DepthTreeVisitor : TreeVisitor 
    {
        public int Depth { get; set; }

        public override void VisitNode(TreeNode node) {
            base.Visit(node.Left);
            int left_depth = this.Depth;

            base.Visit(node.Right);
            int right_depth = this.Depth;
            
            this.Depth = left_depth > right_depth ? left_depth : right_depth;
            this.Depth++;
        } 

        public override void VisitLeaf(TreeLeaf leaf)
        {
            this.Depth = 0;
        }

    }
}
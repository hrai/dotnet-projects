namespace PartA
{
    public class Node<T>
    {
        public Node<T> LinkedNode { get; set; }
        public T Value { get; set; }

        public Node(Node<T> node, T value)
        {
            LinkedNode = node;
            Value = value;
        }

        public void Add(Node<T> linkedNode)
        {
            if (LinkedNode != null)
            {
                LinkedNode.Add(linkedNode);
            }
            else
            {
                LinkedNode = linkedNode;
            }
        }

        public int Count()
        {
            if (LinkedNode == null)
                return 1;

            return 1 + LinkedNode.Count();
        }
    }
}

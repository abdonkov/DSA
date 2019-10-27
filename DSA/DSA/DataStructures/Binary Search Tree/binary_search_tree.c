#include <stdio.h>
#include <stdlib.h>

typedef struct tree {
	//The Binary Tree struct
	int value;
	struct tree* left;
	struct tree* right;
}*Tree;

Tree initTree(int value) {
    //Initialize a binary tree
    Tree a = (Tree)malloc(sizeof(struct tree));
    a->value = value;
    a->left = NULL;
    a->right = NULL;
    return a;
}

Tree insert(Tree tree, int value) {
    //Insert in the Binary Search Tree
    if (tree == NULL)
        return initTree(value);
    if (tree->value == value)
        return tree;
    if (tree->value > value)
    {
        if (tree->left == NULL)
        {
            tree->left = initTree(value);
            return tree;
        }
        else
        {
            tree->left = insert(tree->left,value);
            return tree;
        }
    }
    else
    {
        if (tree->right == NULL)
        {
            tree->right = initTree(value);
            return tree;
        }
        else
        {
            tree->right = insert(tree->right,value);
            return tree;
        }
    }
}

//Depth-First-Search:

void preorder(Tree tree) {
    if (tree == NULL)
        return;
    if (tree->left == NULL && tree->right == NULL)
        printf("%d ",tree->value);
    if (tree->left != NULL && tree->right == NULL)
    {
        printf("%d ",tree->value);
        preorder(tree->left);
    }
    if (tree->right != NULL && tree->left == NULL)
    {
        printf("%d ",tree->value);
        preorder(tree->right);
    }
    if (tree->right != NULL && tree->left != NULL)
    {
        printf("%d ",tree->value);
        preorder(tree->left);
        preorder(tree->right);
    }
}

void inorder(Tree tree) {
    if (tree == NULL)
        return;
    if (tree->left == NULL && tree->right == NULL)
        printf("%d ",tree->value);
    if (tree->left != NULL && tree->right == NULL)
    {
        inorder(tree->left);
        printf("%d ",tree->value);
    }
    if (tree->right != NULL && tree->left == NULL)
    {
        printf("%d ",tree->value);
        inorder(tree->right);
    }
    if (tree->right != NULL && tree->left != NULL)
    {
        inorder(tree->left);
        printf("%d ",tree->value);
        inorder(tree->right);
    }
}

void postorder(Tree tree) {
    if (tree == NULL)
        return;
    if (tree->left == NULL && tree->right == NULL)
        printf("%d ",tree->value);
    if (tree->left != NULL && tree->right == NULL)
    {
        postorder(tree->left);
        printf("%d ",tree->value);
    }
    if (tree->right != NULL && tree->left == NULL)
    {
        postorder(tree->right);
        printf("%d ",tree->value);
    }
    if (tree->left != NULL && tree->right != NULL)
    {
        postorder(tree->left);
        postorder(tree->right);
        printf("%d ",tree->value);
    }
}

int contains(Tree tree, int value) {
    //Check if the Binary Search Tree contains a given value
    if (tree == NULL)
        return 0;
    if (tree->value == value)
        return 1;
    if (tree->value != value)
    {
        if (value < tree->value)
        {
            if (tree->left != NULL)
                return contains(tree->left,value);
            else
                return 0;
        }
        else
        {
            if (tree->right != NULL)
                return contains(tree->right,value);
            else
                return 0;
        }
    }
}

int minimum(Tree tree) {
    //Returns the minimum value of the BST
    if (tree->left == NULL)
        return tree->value;
    else
        return minimum(tree->left);
}

int maximum(Tree tree) {
    //Returns the maximum value of the BST
    if (tree->right == NULL)
        return tree->value;
    else
        return maximum(tree->right);
}


int height(Tree tree) {
    //Returns the height of the tree
    if (tree == NULL)
        return 0;
    else
    {
        Tree tmp;
        int left,right;
        tmp = tree;
        left = height(tmp->left);
        right = height(tmp->right);
        if (left >= right)
        {
            left = left + 1;
            return left;
        }
        else
        {
            right = right + 1;
            return right;
        }
    }

}

Tree delete(Tree tree, int value) {
    //Deletes a given value from the BST
    if (tree == NULL)
        return tree;
    if (tree->value > value)
        tree->left = delete(tree->left,value);
    else if (tree->value < value)
        tree->right = delete(tree->right,value);
    else if (tree->left != NULL && tree->right != NULL)
    {
        Tree tmp = tree->right;
        while (tmp->left != NULL)
            tmp = tmp->left;
        tree->value = tmp->value;
        tree->right = delete(tree->right,tmp->value);
    }
    else
    {
        Tree tmp = tree;
        if (tree->left != NULL)
            tree = tree->left;
        else
            tree = tree->right;
        free(tmp);
    }
    return tree;
}

int lowestCommonAncestor(Tree tree, int value1, int value2) {
    //Returns the lowest Common Ancestor
    if (contains(tree,value1) == 0 || contains(tree,value2) == 0)
        return -1;
    int next_stang, next_drept;
    if (tree->left != NULL)
        next_stang = lowestCommonAncestor(tree->left,value1,value2);
    if (tree->right != NULL)
        next_drept = lowestCommonAncestor(tree->right,value1,value2);
    if (tree->left == NULL && tree->right == NULL)
        return -1;
    if (next_stang == -1 && next_drept == -1)
        return tree->value;
    if (next_stang == -1 && next_drept != -1)
        return lowestCommonAncestor(tree->right,value1,value2);
    if (next_drept == -1 && next_stang != -1)
        return lowestCommonAncestor(tree->left,value1,value2);
}

int checkBST(Tree node) {
    //Checks if the Binary Tree is a BST
    if (node == NULL) 
        return 1;
    if (node->left != NULL && node->left->value > node->value)
        return 0; 
    if (node->right != NULL && node->right->value < node->value) 
        return 0; 
    if (!checkBST(node->left) || !checkBST(node->right)) 
        return 0; 
    return 1; 
}

Tree freeTree(Tree tree) {
    //Frees the allocated memory for the Tree.
    if (tree == NULL)
        return tree;
    tree->left = freeTree(tree->left);
    tree->right = freeTree(tree->right);
    free(tree);
    return NULL;
}

int main(void) {
    //A check from the user input
    Tree T = NULL;
    printf("Number of integers you want to insert in the Binary Search Tree:\n");
    int n;
    scanf("%d",&n);
    printf("Integers to insert in the Binary Search Tree.\n");
    int i, aux;
    for (i=0; i<n; i++)
    {
        scanf("%d",&aux);
        T = insert(T,aux);
    }

    printf("DFS:\n");
    printf("Preorder: ");
    preorder(T);
    printf("\nInorder: ");
    inorder(T);
    printf("\nPostorder: ");
    postorder(T);
    printf("\n");

    printf("Value to check if exists in the BST: ");
    scanf("%d",&aux);
    printf("\n");
    if (contains(T,aux))
        printf("%d exists in the Tree\n",aux);
    else
        printf("%d doesn't exist in the Tree\n",aux);

    printf("Max value : %d\n",maximum(T));
    printf("Min value : %d\n",minimum(T));
    printf("The height of the tree : %d\n",height(T));

    printf("Value to delete: ");
    scanf ("%d",&aux);
    delete(T,aux);
    freeTree(T);
    return 0;
}

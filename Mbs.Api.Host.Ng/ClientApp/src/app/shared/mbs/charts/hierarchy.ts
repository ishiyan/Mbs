export interface HierarchyTreeNode {
  value?: number;
  name?: string;
  children?: HierarchyTreeNode[];
}

export interface Hierarchy {
  children?: HierarchyTreeNode[];
}

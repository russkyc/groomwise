// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
// 
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Interfaces.View;

public interface IView
{
    void ClearFields();
    void ClearFields(params string[] fields);
    void Close();
    void Show();
    void Hide();
    bool Focus();
    bool? ShowDialog();
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ICSharpCode.TextEditor
{
  public class BasePaletteSpec
  {
    public Color BackColor;
    public Color ForeColor;
    public Font Font;
  }

  public class VRulerPaletteSpec : BasePaletteSpec
  {

  }

  public class GutterMarginPaletteSpec : BasePaletteSpec
  {
  
  }

  public class IconBarMarginPaletteSpec : BasePaletteSpec
  {
    public Color LineColor;    
  }

  public class FoldMarginPaletteSpec : BasePaletteSpec
  {
    public Color FoldLineColor;
  }

  public class TextViewPaletteSpec : BasePaletteSpec
  {
    public Color CaretMarkerColor;
    public Color SelectionColor;
  }

  public interface ITextEditorPalette
  {
    GutterMarginPaletteSpec  GetGutterMarginSpec();
    FoldMarginPaletteSpec GetFoldMarginSpec();
    IconBarMarginPaletteSpec GetIconBarMarginSpec();
    VRulerPaletteSpec GetVRulerSpec();
    TextViewPaletteSpec GetTextViewPaletteSpec();
  }
}

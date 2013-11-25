using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ICSharpCode.TextEditor;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public class TextEditorPalette : ITextEditorPalette
  {

    #region ITextEditorPalette Members

    public GutterMarginPaletteSpec GetGutterMarginSpec()
    {
      IPalette palette = KryptonManager.CurrentGlobalPalette;
      if (palette == null)
        return null;

      GutterMarginPaletteSpec spec = new GutterMarginPaletteSpec();
      spec.BackColor = palette.GetBackColor1(PaletteBackStyle.ControlClient,PaletteState.Normal);
      spec.ForeColor = palette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

      spec.Font = palette.GetContentLongTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal).;
      return spec;

    }

    public FoldMarginPaletteSpec GetFoldMarginSpec()
    {
      IPalette palette = KryptonManager.CurrentGlobalPalette;
      if (palette == null)
        return null;

      FoldMarginPaletteSpec spec = new GutterMarginPaletteSpec();
      spec.BackColor = palette.GetBackColor1(PaletteBackStyle.ControlClient,PaletteState.Normal);
      spec.ForeColor = palette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

      spec.Font = palette.GetContentLongTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal).;
      spec.FoldLineColor = spec.ForeColor ;
      return spec;

    }

    public IconBarMarginPaletteSpec GetIconBarMarginSpec()
    {
      IPalette palette = KryptonManager.CurrentGlobalPalette;
      if (palette == null)
        return null;

      IconBarMarginPaletteSpec spec = new GutterMarginPaletteSpec();
      spec.BackColor = palette.GetBackColor1(PaletteBackStyle.ControlClient,PaletteState.Normal);
      spec.ForeColor = palette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);
      spec.Font = palette.GetContentLongTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal).;
      spec.LineColor = spec.ForeColor ;
      return spec;
    }

    public VRulerPaletteSpec GetVRulerSpec()
    {
      IPalette palette = KryptonManager.CurrentGlobalPalette;
      if (palette == null)
        return null;

      VRulerPaletteSpec spec = new GutterMarginPaletteSpec();
      spec.BackColor = palette.GetBackColor1(PaletteBackStyle.ControlClient,PaletteState.Normal);
      spec.ForeColor = palette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

      spec.Font = palette.GetContentLongTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal).;
      return spec;
    }

    public TextViewPaletteSpec GetTextViewPaletteSpec()
    {
      IPalette palette = KryptonManager.CurrentGlobalPalette;
      if (palette == null)
        return null;

      TextViewPaletteSpec spec = new GutterMarginPaletteSpec();
      spec.BackColor = palette.GetBackColor1(PaletteBackStyle.ControlClient,PaletteState.Normal);
      spec.ForeColor = palette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

      spec.Font = palette.GetContentLongTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal).;
      return spec;
    }

    #endregion
  }
}

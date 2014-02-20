namespace Thinknet.ControlLibrary.Utilities.XHtmlToXamlParser
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Xml;

    internal static class HtmlCssParser
    {
        private static readonly string[] colors = 
            {
                "aliceblue", "antiquewhite", "aqua", "aquamarine", "azure", "beige", "bisque", "black", "blanchedalmond",
                "blue", "blueviolet", "brown", "burlywood", "cadetblue", "chartreuse", "chocolate", "coral",
                "cornflowerblue", "cornsilk", "crimson", "cyan", "darkblue", "darkcyan", "darkgoldenrod", "darkgray",
                "darkgreen", "darkkhaki", "darkmagenta", "darkolivegreen", "darkorange", "darkorchid", "darkred",
                "darksalmon", "darkseagreen", "darkslateblue", "darkslategray", "darkturquoise", "darkviolet", "deeppink",
                "deepskyblue", "dimgray", "dodgerblue", "firebrick", "floralwhite", "forestgreen", "fuchsia", "gainsboro",
                "ghostwhite", "gold", "goldenrod", "gray", "green", "greenyellow", "honeydew", "hotpink", "indianred",
                "indigo", "ivory", "khaki", "lavender", "lavenderblush", "lawngreen", "lemonchiffon", "lightblue", "lightcoral",
                "lightcyan", "lightgoldenrodyellow", "lightgreen", "lightgrey", "lightpink", "lightsalmon", "lightseagreen",
                "lightskyblue", "lightslategray", "lightsteelblue", "lightyellow", "lime", "limegreen", "linen", "magenta", 
                "maroon", "mediumaquamarine", "mediumblue", "mediumorchid", "mediumpurple", "mediumseagreen", "mediumslateblue",
                "mediumspringgreen", "mediumturquoise", "mediumvioletred", "midnightblue", "mintcream", "mistyrose", "moccasin",
                "navajowhite", "navy", "oldlace", "olive", "olivedrab", "orange", "orangered", "orchid", "palegoldenrod",
                "palegreen", "paleturquoise", "palevioletred", "papayawhip", "peachpuff", "peru", "pink", "plum", "powderblue",
                "purple", "red", "rosybrown", "royalblue", "saddlebrown", "salmon", "sandybrown", "seagreen", "seashell",
                "sienna", "silver", "skyblue", "slateblue", "slategray", "snow", "springgreen", "steelblue", "tan", "teal",
                "thistle", "tomato", "turquoise", "violet", "wheat", "white", "whitesmoke", "yellow", "yellowgreen",
            };

        private static readonly string[] systemColors = 
            {
                "activeborder", "activecaption", "appworkspace", "background", "buttonface", "buttonhighlight", "buttonshadow",
                "buttontext", "captiontext", "graytext", "highlight", "highlighttext", "inactiveborder", "inactivecaption",
                "inactivecaptiontext", "infobackground", "infotext", "menu", "menutext", "scrollbar", "threeddarkshadow",
                "threedface", "threedhighlight", "threedlightshadow", "threedshadow", "window", "windowframe", "windowtext",
            };

        // CSS has five font properties: font-family, font-style, font-variant, font-weight, font-size.
        // An aggregated "font" property lets you specify in one action all the five in combination
        // with additional line-height property.
        // 
        // font-family: [<family-name>,]* [<family-name> | <generic-family>]
        //    generic-family: serif | sans-serif | monospace | cursive | fantasy
        //       The list of families sets priorities to choose fonts;
        //       Quotes not allowed around generic-family names
        // font-style: normal | italic | oblique
        // font-variant: normal | small-caps
        // font-weight: normal | bold | bolder | lighter | 100 ... 900 |
        //    Default is "normal", normal==400
        // font-size: <absolute-size> | <relative-size> | <length> | <percentage>
        //    absolute-size: xx-small | x-small | small | medium | large | x-large | xx-large
        //    relative-size: larger | smaller
        //    length: <point> | <pica> | <ex> | <em> | <points> | <millimeters> | <centimeters> | <inches>
        //    Default: medium
        // font: [ <font-style> || <font-variant> || <font-weight ]? <font-size> [ / <line-height> ]? <font-family>

        private static readonly string[] fontGenericFamilies = { "serif", "sans-serif", "monospace", "cursive", "fantasy" };
        private static readonly string[] fontStyles = { "normal", "italic", "oblique" };
        private static readonly string[] fontVariants = { "normal", "small-caps" };
        private static readonly string[] fontWeights = { "normal", "bold", "bolder", "lighter", "100", "200", "300", "400", "500", "600", "700", "800", "900" };
        private static readonly string[] fontAbsoluteSizes = { "xx-small", "x-small", "small", "medium", "large", "x-large", "xx-large" };
        private static readonly string[] fontRelativeSizes = { "larger", "smaller" };
        private static readonly string[] fontSizeUnits = { "px", "mm", "cm", "in", "pt", "pc", "em", "ex", "%" };

        private static readonly string[] listStyleTypes = { "disc", "circle", "square", "decimal", "lower-roman", "upper-roman", "lower-alpha", "upper-alpha", "none" };
        private static readonly string[] listStylePositions = { "inside", "outside" };

        private static readonly string[] textDecorations = { "none", "underline", "overline", "line-through", "blink" };
        private static readonly string[] textTransforms = { "none", "capitalize", "uppercase", "lowercase" };
        private static readonly string[] textAligns = { "left", "right", "center", "justify" };
        private static readonly string[] verticalAligns = { "baseline", "sub", "super", "top", "text-top", "middle", "bottom", "text-bottom" };
        private static readonly string[] floats = { "left", "right", "none" };
        private static readonly string[] clears = { "none", "left", "right", "both" };
        private static readonly string[] borderStyles = { "none", "dotted", "dashed", "solid", "double", "groove", "ridge", "inset", "outset" };
        private static readonly string[] blocks = { "block", "inline", "list-item", "none" };

        internal static void GetElementPropertiesFromCssAttributes(XmlElement htmlElement, string elementName, CssStylesheet stylesheet, Hashtable localProperties, List<XmlElement> sourceContext)
        {
            string styleFromStylesheet = stylesheet.GetStyle(elementName, sourceContext);
            string styleInline = XmlHelper.GetAttribute(htmlElement, "style");

            // Combine styles from stylesheet and from inline attribute.
            // The order is important - the latter styles will override the former.
            string style = styleFromStylesheet;
            if (styleInline != null)
            {
                style = style == null ? styleInline : (style + ";" + styleInline);
            }

            // Apply local style to current formatting properties
            if (style != null)
            {
                string[] styleValues = style.Split(';');
                foreach (string styleProps in styleValues)
                {
                    string[] styleNameValue = styleProps.Split(':');
                    if (styleNameValue.Length == 2)
                    {
                        string styleName = styleNameValue[0].Trim().ToLower();
                        string styleValue = UnQuote(styleNameValue[1].Trim()).ToLower();
                        int nextIndex = 0;

                        switch (styleName)
                        {
                            case CssProperties.Font:
                                ParseCssFont(styleValue, localProperties);
                                break;
                            case CssProperties.FontFamily:
                                ParseCssFontFamily(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.FontSize:
                                ParseCssSize(styleValue, ref nextIndex, localProperties, CssProperties.FontSize, /*mustBeNonNegative:*/true);
                                break;
                            case CssProperties.FontStyle:
                                ParseCssFontStyle(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.FontWeight:
                                ParseCssFontWeight(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.FontVariant:
                                ParseCssFontVariant(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.LineHeight:
                                ParseCssSize(styleValue, ref nextIndex, localProperties, CssProperties.LineHeight, /*mustBeNonNegative:*/true);
                                break;
                            case CssProperties.WordSpacing:
                                //  Implement word-spacing conversion
                                break;
                            case CssProperties.LetterSpacing:
                                //  Implement letter-spacing conversion
                                break;
                            case CssProperties.Color:
                                ParseCssColor(styleValue, ref nextIndex, localProperties, CssProperties.Color);
                                break;

                            case CssProperties.TextDecoration:
                                ParseCssTextDecoration(styleValue, ref nextIndex, localProperties);
                                break;

                            case CssProperties.TextTransform:
                                ParseCssTextTransform(styleValue, ref nextIndex, localProperties);
                                break;

                            case CssProperties.BackgroundColor:
                                ParseCssColor(styleValue, ref nextIndex, localProperties, CssProperties.BackgroundColor);
                                break;
                            case CssProperties.Background:
                                // TODO: need to parse composite background property
                                ParseCssBackground(styleValue, ref nextIndex, localProperties);
                                break;

                            case CssProperties.TextAlign:
                                ParseCssTextAlign(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.VerticalAlign:
                                ParseCssVerticalAlign(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.TextIndent:
                                ParseCssSize(styleValue, ref nextIndex, localProperties, CssProperties.TextIndent, /*mustBeNonNegative:*/false);
                                break;

                            case CssProperties.Width:
                            case CssProperties.Height:
                                ParseCssSize(styleValue, ref nextIndex, localProperties, styleName, /*mustBeNonNegative:*/true);
                                break;

                            case CssProperties.Margin: // top/right/bottom/left
                                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, styleName);
                                break;
                            case CssProperties.MarginTop:
                            case CssProperties.MarginRight:
                            case CssProperties.MarginBottom:
                            case CssProperties.MarginLeft:
                                ParseCssSize(styleValue, ref nextIndex, localProperties, styleName, /*mustBeNonNegative:*/true);
                                break;

                            case CssProperties.Padding:
                                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, styleName);
                                break;
                            case CssProperties.PaddingTop:
                            case CssProperties.PaddingRight:
                            case CssProperties.PaddingBottom:
                            case CssProperties.PaddingLeft:
                                ParseCssSize(styleValue, ref nextIndex, localProperties, styleName, /*mustBeNonNegative:*/true);
                                break;

                            case CssProperties.Border:
                                ParseCssBorder(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.BorderStyle:
                            case CssProperties.BorderWidth:
                            case CssProperties.BorderColor:
                                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, styleName);
                                break;
                            case CssProperties.BorderTop:
                            case CssProperties.BorderRight:
                            case CssProperties.BorderLeft:
                            case CssProperties.BorderBottom:
                                //  Parse css border style
                                break;

                                // NOTE: css names for elementary border styles have side indications in the middle (top/bottom/left/right)
                                // In our internal notation we intentionally put them at the end - to unify processing in ParseCssRectangleProperty method
                            case CssProperties.BorderTopStyle:
                            case CssProperties.BorderRightStyle:
                            case CssProperties.BorderLeftStyle:
                            case CssProperties.BorderBottomStyle:
                            case CssProperties.BorderTopColor:
                            case CssProperties.BorderRightColor:
                            case CssProperties.BorderLeftColor:
                            case CssProperties.BorderBottomColor:
                            case CssProperties.BorderTopWidth:
                            case CssProperties.BorderRightWidth:
                            case CssProperties.BorderLeftWidth:
                            case CssProperties.BorderBottomWidth:
                                //  Parse css border style
                                break;

                            case CssProperties.Display:
                                //  Implement display style conversion
                                break;

                            case CssProperties.Float:
                                ParseCssFloat(styleValue, ref nextIndex, localProperties);
                                break;
                            case CssProperties.Clear:
                                ParseCssClear(styleValue, ref nextIndex, localProperties);
                                break;

                            case CssProperties.ListStyleType:
                                ParseCssListStyle(styleValue, localProperties);
                                break;
                        }
                    }
                }
            }
        }

        // .................................................................
        //
        // Parsing CSS - Lexical Helpers
        //
        // .................................................................

        // Skips whitespaces in style values
        private static void ParseWhiteSpace(string styleValue, ref int nextIndex)
        {
            while (nextIndex < styleValue.Length && char.IsWhiteSpace(styleValue[nextIndex]))
            {
                nextIndex++;
            }
        }

        /// <summary>
        /// Checks that the <paramref name="word"/> is content of <paramref name="styleValue"/>.
        /// If this is the case than the <paramref name="nextIndex"/> is moved to the start of the next word in <paramref name="styleValue"/>
        /// </summary>
        /// <param name="word">Word to match</param>
        /// <param name="styleValue">String for word matching.</param>
        /// <param name="nextIndex">Start position of next word in styleValue for comparing.</param>
        /// <returns>True or false depending on success or failure of matching</returns>
        private static bool ParseWord(string word, string styleValue, ref int nextIndex)
        {
            ParseWhiteSpace(styleValue, ref nextIndex);

            for (int i = 0; i < word.Length; i++)
            {
                if (!(nextIndex + i < styleValue.Length && word[i] == styleValue[nextIndex + i]))
                {
                    return false;
                }
            }

            if (nextIndex + word.Length < styleValue.Length && char.IsLetterOrDigit(styleValue[nextIndex + word.Length]))
            {
                return false;
            }

            nextIndex += word.Length;
            return true;
        }

        /// <summary>
        /// Checks one of the <paramref name="words"/> is content of <paramref name="styleValue"/>.
        /// If this is the case than the <paramref name="nextIndex"/> is moved to the start of the next word in <paramref name="styleValue"/>
        /// </summary>
        /// <param name="words">The list of words for check in styleValue.</param>
        /// <param name="styleValue">The value with different words.</param>
        /// <param name="nextIndex">Start parsing position in styleValue for the next word.</param>
        /// <returns>Returns the matched word or <code>null</code> if no word is matched.</returns>
        private static string ParseWordEnumeration(IEnumerable<string> words, string styleValue, ref int nextIndex)
        {
            foreach (string word in words)
            {
                if (ParseWord(word, styleValue, ref nextIndex))
                {
                    return word;
                }
            }

            return null;
        }

        private static void ParseWordEnumeration(IEnumerable<string> words, string styleValue, ref int nextIndex, Hashtable localProperties, string attributeName)
        {
            string attributeValue = ParseWordEnumeration(words, styleValue, ref nextIndex);
            if (attributeValue != null)
            {
                localProperties[attributeName] = attributeValue;
            }
        }

        private static string ParseCssSize(string styleValue, ref int nextIndex, bool mustBeNonNegative)
        {
            ParseWhiteSpace(styleValue, ref nextIndex);

            int startIndex = nextIndex;

            // Parse optional munis sign
            if (nextIndex < styleValue.Length && styleValue[nextIndex] == '-')
            {
                nextIndex++;
            }

            if (nextIndex < styleValue.Length && char.IsDigit(styleValue[nextIndex]))
            {
                while (nextIndex < styleValue.Length && (char.IsDigit(styleValue[nextIndex]) || styleValue[nextIndex] == '.'))
                {
                    nextIndex++;
                }

                string number = styleValue.Substring(startIndex, nextIndex - startIndex);

                string unit = ParseWordEnumeration(fontSizeUnits, styleValue, ref nextIndex);
                if (unit == null)
                {
                    unit = "px"; // Assuming pixels by default
                }

                if (mustBeNonNegative && styleValue[startIndex] == '-')
                {
                    return "0";
                }
                else
                {
                    return number + unit;
                }
            }

            return null;
        }

        private static void ParseCssSize(string styleValue, ref int nextIndex, Hashtable localValues, string propertyName, bool mustBeNonNegative)
        {
            string length = ParseCssSize(styleValue, ref nextIndex, mustBeNonNegative);
            if (length != null)
            {
                localValues[propertyName] = length;
            }
        }

        private static string ParseCssColor(string styleValue, ref int nextIndex)
        {
            //  Implement color parsing
            // rgb(100%,53.5%,10%)
            // rgb(255,91,26)
            // #FF5B1A
            // black | silver | gray | ... | aqua
            // transparent - for background-color
            ParseWhiteSpace(styleValue, ref nextIndex);

            string color = null;

            if (nextIndex < styleValue.Length)
            {
                int startIndex = nextIndex;
                char character = styleValue[nextIndex];

                if (character == '#')
                {
                    nextIndex++;
                    while (nextIndex < styleValue.Length)
                    {
                        character = char.ToUpper(styleValue[nextIndex]);
                        if (!(('0' <= character && character <= '9') || ('A' <= character && character <= 'F')))
                        {
                            break;
                        }

                        nextIndex++;
                    }

                    if (nextIndex > startIndex + 1)
                    {
                        color = styleValue.Substring(startIndex, nextIndex - startIndex);
                    }
                }
                else if (styleValue.Substring(nextIndex, 3).ToLower() == "rbg")
                {
                    //  Implement real rgb() color parsing
                    while (nextIndex < styleValue.Length && styleValue[nextIndex] != ')')
                    {
                        nextIndex++;
                    }

                    if (nextIndex < styleValue.Length)
                    {
                        nextIndex++; // to skip ')'
                    }

                    color = "gray"; // return bogus color
                }
                else if (char.IsLetter(character))
                {
                    color = ParseWordEnumeration(colors, styleValue, ref nextIndex);
                    if (color == null)
                    {
                        color = ParseWordEnumeration(systemColors, styleValue, ref nextIndex);
                        if (color != null)
                        {
                            //  Implement smarter system color converions into real colors
                            color = "black";
                        }
                    }
                }
            }

            return color;
        }

        private static void ParseCssColor(string styleValue, ref int nextIndex, Hashtable localValues, string propertyName)
        {
            string color = ParseCssColor(styleValue, ref nextIndex);
            if (color != null)
            {
                localValues[propertyName] = color;
            }
        }
        
        /// <summary>
        /// Parses CSS string fontStyle representing a value for css font attribute
        /// </summary>
        /// <param name="styleValue">String containing all font properties in a single line.</param>
        /// <param name="localProperties">Parsed font properties will be filled in the local properties map.</param>
        private static void ParseCssFont(string styleValue, Hashtable localProperties)
        {
            int nextIndex = 0;

            ParseCssFontStyle(styleValue, ref nextIndex, localProperties);
            ParseCssFontVariant(styleValue, ref nextIndex, localProperties);
            ParseCssFontWeight(styleValue, ref nextIndex, localProperties);

            ParseCssSize(styleValue, ref nextIndex, localProperties, CssProperties.FontSize, /*mustBeNonNegative:*/true);

            ParseWhiteSpace(styleValue, ref nextIndex);
            if (nextIndex < styleValue.Length && styleValue[nextIndex] == '/')
            {
                nextIndex++;
                ParseCssSize(styleValue, ref nextIndex, localProperties, CssProperties.LineHeight, /*mustBeNonNegative:*/true);
            }

            ParseCssFontFamily(styleValue, ref nextIndex, localProperties);
        }

        private static void ParseCssFontStyle(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(fontStyles, styleValue, ref nextIndex, localProperties, CssProperties.FontStyle);
        }

        private static void ParseCssFontVariant(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(fontVariants, styleValue, ref nextIndex, localProperties, CssProperties.FontVariant);
        }

        private static void ParseCssFontWeight(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(fontWeights, styleValue, ref nextIndex, localProperties, CssProperties.FontWeight);
        }

        private static void ParseCssFontFamily(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            string fontFamilyList = null;

            while (nextIndex < styleValue.Length)
            {
                // Try generic-family
                string fontFamily = ParseWordEnumeration(fontGenericFamilies, styleValue, ref nextIndex);

                if (fontFamily == null)
                {
                    // Try quoted font family name
                    if (nextIndex < styleValue.Length && (styleValue[nextIndex] == '"' || styleValue[nextIndex] == '\''))
                    {
                        char quote = styleValue[nextIndex];

                        nextIndex++;

                        int startIndex = nextIndex;

                        while (nextIndex < styleValue.Length && styleValue[nextIndex] != quote)
                        {
                            nextIndex++;
                        }

                        fontFamily = '"' + styleValue.Substring(startIndex, nextIndex - startIndex) + '"';
                    }

                    if (fontFamily == null)
                    {
                        // Try unquoted font family name
                        int startIndex = nextIndex;
                        while (nextIndex < styleValue.Length && styleValue[nextIndex] != ',' && styleValue[nextIndex] != ';')
                        {
                            nextIndex++;
                        }

                        if (nextIndex > startIndex)
                        {
                            fontFamily = styleValue.Substring(startIndex, nextIndex - startIndex).Trim();
                            if (fontFamily.Length == 0)
                            {
                                fontFamily = null;
                            }
                        }
                    }
                }

                ParseWhiteSpace(styleValue, ref nextIndex);
                if (nextIndex < styleValue.Length && styleValue[nextIndex] == ',')
                {
                    nextIndex++;
                }

                if (fontFamily != null)
                {
                    //  css font-family can contein a list of names. We only consider the first name from the list. Need a decision what to do with remaining names
                    // fontFamilyList = (fontFamilyList == null) ? fontFamily : fontFamilyList + "," + fontFamily;
                    if (fontFamilyList == null && fontFamily.Length > 0)
                    {
                        if (fontFamily[0] == '"' || fontFamily[0] == '\'')
                        {
                            // Unquote the font family name
                            fontFamily = fontFamily.Substring(1, fontFamily.Length - 2);
                        }

                        fontFamilyList = fontFamily;
                    }
                }
                else
                {
                    break;
                }
            }

            if (fontFamilyList != null)
            {
                localProperties[CssProperties.FontFamily] = fontFamilyList;
            }
        }

        /// <summary>
        /// Parses the CSS list-style-xxx properties.
        /// </summary>
        /// <param name="styleValue">The value of the list-style-xxx property.</param>
        /// <param name="localProperties">The properties for setting the parsed property values.</param>
        private static void ParseCssListStyle(string styleValue, Hashtable localProperties)
        {
            int nextIndex = 0;

            while (nextIndex < styleValue.Length)
            {
                string listStyleType = ParseCssListStyleType(styleValue, ref nextIndex);
                if (listStyleType != null)
                {
                    localProperties["list-style-type"] = listStyleType;
                }
                else
                {
                    string listStylePosition = ParseCssListStylePosition(styleValue, ref nextIndex);
                    if (listStylePosition != null)
                    {
                        localProperties["list-style-position"] = listStylePosition;
                    }
                    else
                    {
                        string listStyleImage = ParseCssListStyleImage(styleValue, ref nextIndex);
                        if (listStyleImage != null)
                        {
                            localProperties["list-style-image"] = listStyleImage;
                        }
                        else
                        {
                            // TODO: Process unrecognized list style value
                            break;
                        }
                    }
                }
            }
        }

        private static string ParseCssListStyleType(string styleValue, ref int nextIndex)
        {
            return ParseWordEnumeration(listStyleTypes, styleValue, ref nextIndex);
        }

        private static string ParseCssListStylePosition(string styleValue, ref int nextIndex)
        {
            return ParseWordEnumeration(listStylePositions, styleValue, ref nextIndex);
        }

        private static string ParseCssListStyleImage(string styleValue, ref int nextIndex)
        {
            // TODO: Implement URL parsing for images
            return null;
        }

        private static void ParseCssTextDecoration(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            // Set default text-decorations:none;
            for (int i = 1; i < textDecorations.Length; i++)
            {
                localProperties["text-decoration-" + textDecorations[i]] = "false";
            }

            // Parse list of decorations values
            while (nextIndex < styleValue.Length)
            {
                string decoration = ParseWordEnumeration(textDecorations, styleValue, ref nextIndex);
                if (decoration == null || decoration == "none")
                {
                    break;
                }

                localProperties["text-decoration-" + decoration] = "true";
            }
        }

        private static void ParseCssTextTransform(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(textTransforms, styleValue, ref nextIndex, localProperties, CssProperties.TextTransform);
        }

        private static void ParseCssTextAlign(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(textAligns, styleValue, ref nextIndex, localProperties, CssProperties.TextAlign);
        }

        private static void ParseCssVerticalAlign(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            //  Parse percentage value for vertical-align style
            ParseWordEnumeration(verticalAligns, styleValue, ref nextIndex, localProperties, CssProperties.VerticalAlign);
        }

        private static void ParseCssFloat(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(floats, styleValue, ref nextIndex, localProperties, CssProperties.Float);
        }

        private static void ParseCssClear(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(clears, styleValue, ref nextIndex, localProperties, CssProperties.Clear);
        }

        // .................................................................
        //
        // Pasring CSS margin and padding Properties
        //
        // .................................................................

        // Generic method for parsing any of four-values properties, such as margin, padding, border-width, border-style, border-color
        private static bool ParseCssRectangleProperty(string styleValue, ref int nextIndex, Hashtable localProperties, string propertyName)
        {
            // CSS Spec: 
            // If only one value is set, then the value applies to all four sides;
            // If two or three values are set, then missinng value(s) are taken fromm the opposite side(s).
            // The order they are applied is: top/right/bottom/left

            Contract.Assert(propertyName == CssProperties.Margin || propertyName == CssProperties.Padding || propertyName == CssProperties.BorderWidth || propertyName == CssProperties.BorderStyle || propertyName == CssProperties.BorderColor);

            string value = propertyName == CssProperties.BorderColor ? ParseCssColor(styleValue, ref nextIndex) : propertyName == CssProperties.BorderStyle ? ParseCssBorderStyle(styleValue, ref nextIndex) : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
            if (value != null)
            {
                localProperties[propertyName + "-top"] = value;
                localProperties[propertyName + "-bottom"] = value;
                localProperties[propertyName + "-right"] = value;
                localProperties[propertyName + "-left"] = value;
                value = propertyName == CssProperties.BorderColor ? ParseCssColor(styleValue, ref nextIndex) : propertyName == CssProperties.BorderStyle ? ParseCssBorderStyle(styleValue, ref nextIndex) : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
                if (value != null)
                {
                    localProperties[propertyName + "-right"] = value;
                    localProperties[propertyName + "-left"] = value;
                    value = propertyName == CssProperties.BorderColor ? ParseCssColor(styleValue, ref nextIndex) : propertyName == CssProperties.BorderStyle ? ParseCssBorderStyle(styleValue, ref nextIndex) : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
                    if (value != null)
                    {
                        localProperties[propertyName + "-bottom"] = value;
                        value = propertyName == CssProperties.BorderColor ? ParseCssColor(styleValue, ref nextIndex) : propertyName == CssProperties.BorderStyle ? ParseCssBorderStyle(styleValue, ref nextIndex) : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
                        if (value != null)
                        {
                            localProperties[propertyName + "-left"] = value;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        private static void ParseCssBorder(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            while (
                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, CssProperties.BorderWidth) ||
                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, CssProperties.BorderStyle) ||
                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, CssProperties.BorderColor))
            {
            }
        }

        private static string ParseCssBorderStyle(string styleValue, ref int nextIndex)
        {
            return ParseWordEnumeration(borderStyles, styleValue, ref nextIndex);
        }

        private static void ParseCssBackground(string styleValue, ref int nextIndex, Hashtable localValues)
        {
            //  Implement parsing background attribute
        }

        /// <summary>
        /// Returns string extracted from quotation marks
        /// </summary>
        /// <param name="value">
        /// String representing value enclosed in quotation marks
        /// </param>
        private static string UnQuote(string value)
        {
            if ((value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("'") && value.EndsWith("'")))
            {
                value = value.Substring(1, value.Length - 2).Trim();
            }

            return value;
        }
    }
}

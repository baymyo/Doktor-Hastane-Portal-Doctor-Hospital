<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomizeControl.ascx.cs"
    Inherits="CustomizeControl" %>
<asp:Literal ID="CustomMessage" runat="server" Text="" />
<div class="clear">
</div>
<asp:Panel ID="CustomControls" runat="server" DefaultButton="CustomSubmit">
    <%= (FormTitleVisible) ? "<fieldset><legend>" + FormTitle + "</legend>" : ""%>
    <asp:Panel ID="CustomPanel" runat="server">
    </asp:Panel>
    <div class="clear">
    </div>
    <asp:Label ID="CustomStatus" runat="server" CssClass="statusLabel left" Text=""></asp:Label>
    <div class="button-set">
        <script type="text/javascript">
            (function ($) {
                $.fn.extend({ filter_input: function (options) {
                    var defaults = {
                        regex: ".*",
                        live: false
                    }
                    var options = $.extend(defaults, options);
                    var regex = new RegExp(options.regex);
                    function filter_input_function(event) {
                        var key = event.charCode ? event.charCode : event.keyCode ? event.keyCode : 0;
                        // 8 = backspace, 9 = tab, 13 = enter, 35 = end, 36 = home, 37 = left, 39 = right, 46 = delete
                        if (key == 8 || key == 9 || key == 13 || key == 35 || key == 36 || key == 37 || key == 39 || key == 46) {
                            if ($.browser.mozilla) {
                                // if charCode = key & keyCode = 0
                                // 35 = #, 36 = $, 37 = %, 39 = ', 46 = .
                                if (event.charCode == 0 && event.keyCode == key) {
                                    return true;
                                }
                            }
                        }
                        var string = String.fromCharCode(key);
                        if (regex.test(string)) {
                            return true;
                        } else if (typeof (options.feedback) == 'function') {
                            options.feedback.call(this, string);
                        }
                        return false;
                    }

                    if (options.live) {
                        $(this).live('keypress', filter_input_function);
                    } else {
                        return this.each(function () {
                            var input = $(this);
                            input.unbind('keypress').keypress(filter_input_function);
                        });
                    }
                }
                });
            })(jQuery); 
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                function format(item) {
                    return item.CompanyName;
                }

                $('.submitButton').click(function () {
                    $(".error").hide();
                    var handle = true;
                    $('.emptyValidate').each(function () {
                        if ($(this).val() == '') {
                            $(this).after('<span class="error">Bu alan boş bırakılamaz.</span>');
                            handle = false;
                        }
                    });

                    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                    $('.mailValidate').each(function () {
                        if (!emailReg.test($(this).val())) {
                            $(this).after('<span class="error">Lütfen geçerli "e-Mail" adresi giriniz.</span>');
                            handle = false;
                        }
                    });

                    var noHtmlReg = /^[^<>]*$/;
                    $('.noHtml').each(function () {
                        if (!noHtmlReg.test($(this).val())) {
                            $(this).after('<span class="error">Lütfen geçerli "HTML" içerik girmeyiniz.</span>');
                            handle = false;
                        }
                    });

                    return handle;
                });
            });
            $('.isNumber').filter_input({ regex: '[0-9]' });
            $('.smallCharNumber').filter_input({ regex: '[a-z0-9]' });
        </script>
        <asp:Button ID="CustomSubmit" runat="server" Text="Kaydet" CssClass="right" OnClick="CustomSubmit_Click" />
        <asp:Button ID="CustomUpdate" runat="server" Text="Güncelle" CssClass="right" OnClick="CustomUpdate_Click" Visible="false" />
        <asp:Button ID="CustomRemove" runat="server" Text="Sil" CssClass="removeButton right" OnClick="CustomRemove_Click" />
    </div>
    <%= (FormTitleVisible) ? "</fieldset>" : ""%>
</asp:Panel>

<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_NewsLetterUaep" Codebehind="~/CMSWebParts/NewsLetterUaep.ascx.cs" %>
<div class="form-wrapper">

    <div id="NewsLetterform">
        <div class="animate" data-animation="fadeInUp" data-duration="600">
            <div class="row no-gutters">
                <div class="col-12 col-lg-4">
                    <h3 id="NewsletterTitle" runat="server"></h3>
                </div>
                <div class="col-12 col-md-8 col-lg-5" >
                    <div class="form-group">
                        <input type="text" id="inp" runat="server" name="email" class="form-control" placeholder= "" ClientIDMode="Static" />
                        
                    </div>
                    <div class="alreadyExist" style="display: none;">
                        <span style="float: inherit;padding-top: 6px;color: red;font-size: 13px;background: #FFF;padding: 5px 11px;display: inline-block;line-height: 1;margin: 3px 0 0;border-radius: 2px;font-weight: 600;" id="alreadyregistermessage" runat="server">Your are already registered</span>
                    </div>

                </div>
                <div class="col-12 col-md-4 col-lg-3 col-xl-2 text-center">
                    <button type="submit" class="btn btn-outline-primary submit-btn">
                        <span id="NewsletterCtaTitle" runat="server" clientidmode="Static">Submit</span>
                    </button>
                </div>
            </div>
        </div>
        <div>
        </div>

        <div class="thanks">
            <div class="thanks-inner">
                <h4 class="mb-1" id="newsletterthnksmsg" runat="server">Thanks For Subscription!</h4>
            </div>
        </div>

    </div>
</div>
</div>

<%@ Page Title="Home - CartPro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CartProWebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CartPro - Fashion & Style Collection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Start Hero Section -->
    <div class="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-5">
                    <div class="intro-excerpt">
                        <h1>Fashion &amp; Style <span class="d-block"></span>Collection</h1>
                        <p class="mb-4">Discover our curated selection of premium shoes, perfumes, belts, and watches to elevate your style and complement your modern lifestyle.</p>
                        <p><a href="Shop.aspx" class="btn btn-secondary me-2">Shop Now</a><a href="#" class="btn btn-white-outline">Explore</a></p>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="hero-img-wrap">
                        <img src="images/couch.png" class="img-fluid" style="width: 90%; margin-left: 15%;" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Hero Section -->

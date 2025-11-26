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
    <!-- Start Product Section -->
    <div class="product-section">
        <div class="container">
            <div class="row">
                <!-- Start Column 1 -->
                <div class="col-md-12 col-lg-3 mb-5 mb-lg-0">
                    <h2 class="mb-4 section-title">Explore Our Collections</h2>
                    <p class="mb-4">From timeless watches to elegant shoes, our collections are crafted to perfection. Discover accessories that define your style.</p>
                    <p><a href="Shop.aspx" class="btn">Explore</a></p>
                </div>
                <!-- End Column 1 -->
                <!-- Start Column 2 -->
                <div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
                    <a class="product-item" href="Cart.aspx">
                        <img src="images/product-1.png" class="img-fluid product-thumbnail" />
                        <h3 class="product-title">Classic Leather Shoes</h3>
                        <strong class="product-price">$120.00</strong>
                        <span class="icon-cross">
                            <img src="images/cross.svg" class="img-fluid" />
                        </span>
                    </a>
                </div>
                <!-- End Column 2 -->



package pt.isel.markettracker.ui.screens.product

import android.util.Log
import pt.isel.markettracker.domain.model.market.inventory.product.Product
import pt.isel.markettracker.domain.model.market.inventory.product.ProductPreferences
import pt.isel.markettracker.domain.model.market.inventory.product.ProductReview
import pt.isel.markettracker.domain.model.market.inventory.product.ProductStats
import pt.isel.markettracker.domain.model.market.price.CompanyPrices
import pt.isel.markettracker.domain.model.market.price.PriceAlert

sealed class ProductDetailsScreenState {
    data object Idle : ProductDetailsScreenState()
    data object LoadingProduct : ProductDetailsScreenState()
    data class LoadedProduct(val product: Product) : ProductDetailsScreenState()
    data class LoadingProductDetails(
        val product: Product,
        val prices: List<CompanyPrices>? = null,
        val stats: ProductStats? = null,
        val preferences: ProductPreferences? = null,
        val alerts: List<PriceAlert>? = null,
        val reviews: List<ProductReview>? = null
    ) : ProductDetailsScreenState()

    abstract class Loaded(
        open val product: Product,
        open val prices: List<CompanyPrices>,
        open val stats: ProductStats,
        open val preferences: ProductPreferences,
        open val alerts: List<PriceAlert>,
        open val reviews: List<ProductReview>
    ) : ProductDetailsScreenState()

    data class LoadedDetails(
        override val product: Product,
        override val prices: List<CompanyPrices>,
        override val stats: ProductStats,
        override val preferences: ProductPreferences,
        override val alerts: List<PriceAlert>,
        override val reviews: List<ProductReview>
    ) : Loaded(product, prices, stats, preferences, alerts, reviews)

    data class SubmittingPriceAlert(
        override val product: Product,
        override val prices: List<CompanyPrices>,
        override val stats: ProductStats,
        override val preferences: ProductPreferences,
        override val alerts: List<PriceAlert>,
        override val reviews: List<ProductReview>
    ) : Loaded(product, prices, stats, preferences, alerts, reviews)

    data class SubmittingReview(
        override val product: Product,
        override val prices: List<CompanyPrices>,
        override val stats: ProductStats,
        override val preferences: ProductPreferences,
        override val alerts: List<PriceAlert>,
        override val reviews: List<ProductReview>
    ) : Loaded(product, prices, stats, preferences, alerts, reviews)

    data class FailedToLoadProduct(val error: Throwable) : ProductDetailsScreenState()
    data class Failed(val error: Throwable) : ProductDetailsScreenState()
}

/**
 * Transforms a [ProductDetailsScreenState.LoadingProductDetails] into a [ProductDetailsScreenState.LoadedDetails]
 * if all the required data is finally available.
 */
fun ProductDetailsScreenState.LoadingProductDetails.toLoadedDetailsIfReady(): ProductDetailsScreenState {
    return if (this.prices == null || this.stats == null || this.preferences == null || this.alerts == null || this.reviews == null) {
        this
    } else {
        ProductDetailsScreenState.LoadedDetails(
            product = product,
            prices = prices,
            stats = stats,
            preferences = preferences,
            alerts = alerts,
            reviews = reviews
        )
    }
}

fun ProductDetailsScreenState.LoadedDetails.toSubmittingPriceAlert(): ProductDetailsScreenState {
    return ProductDetailsScreenState.SubmittingPriceAlert(
        product = product,
        prices = prices,
        stats = stats,
        preferences = preferences,
        alerts = alerts,
        reviews = reviews
    )
}

fun ProductDetailsScreenState.LoadedDetails.toSubmittingReview(): ProductDetailsScreenState {
    return ProductDetailsScreenState.SubmittingReview(
        product = product,
        prices = prices,
        stats = stats,
        preferences = preferences,
        alerts = alerts,
        reviews = reviews
    )
}

fun ProductDetailsScreenState.extractProduct(): Product? {
    return when (this) {
        is ProductDetailsScreenState.LoadedProduct -> product
        is ProductDetailsScreenState.LoadingProductDetails -> product
        is ProductDetailsScreenState.Loaded -> product
        else -> null
    }
}

fun ProductDetailsScreenState.extractPrices(): List<CompanyPrices>? {
    return when (this) {
        is ProductDetailsScreenState.LoadingProductDetails -> prices
        is ProductDetailsScreenState.Loaded -> prices
        else -> null
    }
}

fun ProductDetailsScreenState.extractStats(): ProductStats? {
    return when (this) {
        is ProductDetailsScreenState.LoadingProductDetails -> stats
        is ProductDetailsScreenState.Loaded -> stats
        else -> null
    }
}

fun ProductDetailsScreenState.extractPreferences(): ProductPreferences? {
    return when (this) {
        is ProductDetailsScreenState.LoadingProductDetails -> preferences
        is ProductDetailsScreenState.Loaded -> preferences
        else -> null
    }
}

fun ProductDetailsScreenState.extractAlerts(): List<PriceAlert>? {
    return when (this) {
        is ProductDetailsScreenState.LoadingProductDetails -> alerts
        is ProductDetailsScreenState.Loaded -> alerts
        else -> null
    }
}

fun ProductDetailsScreenState.extractReviews(): List<ProductReview>? {
    return when (this) {
        is ProductDetailsScreenState.LoadingProductDetails -> reviews
        is ProductDetailsScreenState.Loaded -> reviews
        else -> null
    }
}
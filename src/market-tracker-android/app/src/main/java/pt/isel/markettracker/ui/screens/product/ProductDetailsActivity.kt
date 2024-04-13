package pt.isel.markettracker.ui.screens.product

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import dagger.hilt.android.AndroidEntryPoint
import pt.isel.markettracker.ui.theme.MarkettrackerTheme

@AndroidEntryPoint
class ProductDetailsActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContent {
            MarkettrackerTheme {
                ProductDetailsScreen(onBackRequested = { finish() })
            }
        }
    }
}
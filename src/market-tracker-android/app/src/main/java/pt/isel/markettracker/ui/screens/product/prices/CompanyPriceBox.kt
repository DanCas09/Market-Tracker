package pt.isel.markettracker.ui.screens.product.prices

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.gestures.detectTapGestures
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.RichTooltip
import androidx.compose.material3.Text
import androidx.compose.material3.TooltipBox
import androidx.compose.material3.TooltipDefaults.rememberPlainTooltipPositionProvider
import androidx.compose.material3.rememberTooltipState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.pointer.pointerInput
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import kotlinx.coroutines.launch
import pt.isel.markettracker.R
import pt.isel.markettracker.domain.model.market.price.Price
import pt.isel.markettracker.domain.model.market.price.Promotion
import pt.isel.markettracker.ui.components.buttons.AddToListButton
import pt.isel.markettracker.ui.screens.products.card.PriceLabel
import pt.isel.markettracker.utils.timeSince
import java.time.LocalDateTime

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun CompanyPriceBox(
    price: Price,
    lastChecked: LocalDateTime,
    showOptions: Boolean,
    onAddToListClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    val positionProvider = rememberPlainTooltipPositionProvider()
    val tooltipState = rememberTooltipState()
    val scope = rememberCoroutineScope()

    Row(
        verticalAlignment = Alignment.CenterVertically,
        horizontalArrangement = Arrangement.spacedBy(8.dp),
        modifier = modifier
    ) {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.spacedBy(4.dp),
            modifier = Modifier
                .align(Alignment.Bottom)
        ) {
            Row(
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.spacedBy(4.dp),
            ) {
                TooltipBox(
                    positionProvider = positionProvider,
                    tooltip = {
                        RichTooltip {
                            Text(
                                "${stringResource(id = R.string.verified_at)} ${
                                    timeSince(lastChecked)
                                }"
                            )
                        }
                    },
                    state = tooltipState
                ) {
                    Box(
                        modifier = Modifier.pointerInput(Unit) {
                            detectTapGestures(
                                onLongPress = {
                                    scope.launch {
                                        tooltipState.show()
                                    }
                                }
                            )
                        }
                    ) {
                        PriceLabel(price, true)
                    }
                }
            }

            AnimatedVisibility(visible = showOptions) {
                AddToListButton(onClick = onAddToListClick)
            }
        }
    }
}

@Preview
@Composable
fun CompanyPriceBoxPreview() {
    CompanyPriceBox(
        price = Price(100, 50, Promotion(10, LocalDateTime.now()), LocalDateTime.now()),
        lastChecked = LocalDateTime.now(),
        showOptions = true,
        onAddToListClick = {}
    )
}
package pt.isel.markettracker.ui.screens.alerts

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import pt.isel.markettracker.ui.components.buttons.MarketTrackerBackButton
import pt.isel.markettracker.ui.screens.alerts.list.AlertsListView
import pt.isel.markettracker.ui.screens.products.topbar.HeaderLogo
import pt.isel.markettracker.ui.theme.mainFont

@Composable
fun ListAlertsScreenView(
    state: AlertsScreenState,
    onRemoveFromAlertsRequested: (String) -> Unit,
    onBackRequested: () -> Unit,
) {
    Scaffold(
        topBar =
        {
            Row(
                modifier = Modifier
                    .fillMaxWidth()
                    .background(Color.Red)
                    .padding(10.dp),
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.spacedBy(14.dp)
            ) {
                Box(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    HeaderLogo(
                        modifier = Modifier
                            .align(alignment = Alignment.CenterStart)
                            .size(48.dp)
                    )
                    Text(
                        text = "Alertas 🔔",
                        color = Color.White,
                        fontFamily = mainFont,
                        fontSize = 30.sp,
                        modifier = Modifier
                            .align(alignment = Alignment.Center)
                    )

                    Box(
                        modifier = Modifier.align(alignment = Alignment.CenterEnd)
                    ) {
                        MarketTrackerBackButton(onBackRequested = onBackRequested)
                    }
                }
            }
        },
    ) { paddingValues ->

        Box(
            modifier = Modifier
                .fillMaxWidth()
                .padding(paddingValues),
            contentAlignment = Alignment.Center
        ) {
            AlertsListView(
                state = state,
                onDeleteProductFromAlertsRequested = onRemoveFromAlertsRequested
            )
        }
    }
}
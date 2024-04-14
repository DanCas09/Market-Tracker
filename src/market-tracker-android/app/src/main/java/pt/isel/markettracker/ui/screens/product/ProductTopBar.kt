package pt.isel.markettracker.ui.screens.product

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AddAlert
import androidx.compose.material.icons.filled.ArrowBackIosNew
import androidx.compose.material.icons.filled.Favorite
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Surface
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import pt.isel.markettracker.ui.theme.Grey

@Composable
fun ProductTopBar(onBackRequest: () -> Unit) {
    Surface(color = Color.White) {
        Row {
            IconButton(
                onClick = onBackRequest, modifier = Modifier
                    .padding(4.dp)
                    .background(Grey, shape = CircleShape)
            ) {
                Icon(
                    imageVector = Icons.Default.ArrowBackIosNew,
                    contentDescription = "Back"
                )
            }
            Spacer(modifier = Modifier.weight(1f))

            IconButton(onClick = { /*TODO*/ }, modifier = Modifier.padding(8.dp)) {
                Icon(
                    imageVector = Icons.Default.AddAlert,
                    contentDescription = "Alert",
                )
            }

            IconButton(onClick = { /*TODO*/ }, modifier = Modifier.padding(8.dp)) {
                Icon(
                    imageVector = Icons.Default.Favorite,
                    contentDescription = "Favorite",
                )
            }
        }
    }
}
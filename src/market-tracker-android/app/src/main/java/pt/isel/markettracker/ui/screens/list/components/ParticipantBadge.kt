package pt.isel.markettracker.ui.screens.list.components

import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.offset
import androidx.compose.foundation.layout.size
import androidx.compose.material3.BadgedBox
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import pt.isel.markettracker.R

@Composable
fun ParticipantBadge(numberOfParticipants: Int) {
    BadgedBox(
        badge = {
            Text(
                text = numberOfParticipants.toString(),
                modifier = Modifier.offset(x = (0).dp, y = (-10).dp),
                style = TextStyle(fontSize = 15.sp)
            )
        }
    ) {
        Image(
            painter = painterResource(id = R.drawable.person),
            contentDescription = "",
            modifier = Modifier
                .size(32.dp)
        )
    }
}
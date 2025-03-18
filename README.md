### **Player Movement Script for Unity**  

This script provides **first-person movement** for a player using the **CharacterController** component in Unity. It includes:  
✔️ Basic movement (WASD)  
✔️ Sprinting with limited stamina  
✔️ Dynamic Field of View (FOV) changes when sprinting  
✔️ Gravity simulation  
✔️ Stair detection and smooth climbing  

---

## **🎮 Features**  

- **Smooth First-Person Movement:** Uses Unity's `CharacterController` for smooth motion.  
- **Sprinting with Stamina:** Sprint depletes over time and regenerates when not in use.  
- **Realistic Gravity:** Keeps the player grounded and prevents floating.  
- **Stair Climbing:** Detects stairs and allows smooth vertical movement.  
- **FOV Adjustment:** Field of View increases while sprinting for a more immersive feel.  

---

## **📜 How to Use**  

1. **Attach the Script**: Add the `PlayerMovement` script to the **player GameObject** in Unity.  
2. **Add Components**: Ensure the player has:  
   - A **CharacterController** component.  
   - A **Camera** attached as the player's view.  
   - A **Ground Check** empty GameObject under the player.  
3. **Configure Variables**: Adjust movement speed, sprint duration, FOV values, and gravity in the **Inspector**.  

---

## **🎮 Controls**  

| Action   | Key |
|----------|-----|
| Move     | `W A S D` |
| Sprint   | `Left Shift` |
| Look Around | `Mouse` |

---

## **🛠️ Setup in Unity**  

1. **Create an Empty Object** under the player:  
   - Name it `GroundCheck` and position it at the player's feet.  
   - Assign it to the `groundCheck` variable in the script.  
2. **Set up Layers**:  
   - Add a **"Ground" layer** and assign it to ground objects.  
   - Add a **"Stairs" layer** and assign it to stair objects.  
   - Set `groundMask` to **Ground** and `stairsMask` to **Stairs** in the script.  
3. **Ensure the Player Has a CharacterController** component.  


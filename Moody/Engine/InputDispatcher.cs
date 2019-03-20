using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moody.Delegates;
using Moody.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Engine
{
    public class InputDispatcher
    {
        private Dictionary<Keys, KeyDownEventHandler> keyDownEvents = new Dictionary<Keys, KeyDownEventHandler>();
        private Dictionary<Keys, KeyUpEventHandler> keyUpEvents = new Dictionary<Keys, KeyUpEventHandler>();
        private Dictionary<Keys, KeyHeldDelegate> keyHeldDelegates = new Dictionary<Keys, KeyHeldDelegate>();
        private Dictionary<InputAxes, AxisDelegate> axisDelegates = new Dictionary<InputAxes, AxisDelegate>();
        private KeyboardState previousKeyboardState = new KeyboardState();
        private KeyboardState currentKeyboardState = new KeyboardState();
        private MouseState previousMouseState;
        private MouseState currentMouseState;
        private Vector2 previousMousePosition = Vector2.Zero;
        private Vector2 currentMousePosition = Vector2.Zero;
        private Vector2 deltaMousePosition = Vector2.Zero;

        private Dictionary<InputAxes, Axis> axes = new Dictionary<InputAxes, Axis>();
        private Vector2 viewportDimensions = Vector2.Zero;
        private bool siezeMouse = true;

        private Vector2 target = new Vector2(200,200);

        public Dictionary<InputAxes, Axis> Axes { get => axes; set => axes = value; }
        public Vector2 ViewportDimensions { get => viewportDimensions; set => viewportDimensions = value; }
        public bool SiezeMouse { get => siezeMouse; set => siezeMouse = value; }

        public void SubscribeKeyDownEvent(Keys key, KeyDownEventHandler keyDownEventHandler)
        {
            KeyDownEventHandler foo;
            if (keyDownEvents.TryGetValue(key, out foo))
            {
                keyDownEvents[key] += keyDownEventHandler;
            }
            else
            {
                keyDownEvents.Add(key, keyDownEventHandler);
            }
        }
        public void SubscribeKeyUpEvent(Keys key, KeyUpEventHandler keyUpEventHandler)
        {
            KeyUpEventHandler foo;
            if (keyUpEvents.TryGetValue(key, out foo))
            {
                keyUpEvents[key] += keyUpEventHandler;
            }
            else
            {
                keyUpEvents.Add(key, keyUpEventHandler);
            }
        }
        public void SubscribeKeyHeldDelegate(Keys key, KeyHeldDelegate keyHeldDelegate)
        {
            KeyHeldDelegate foo;
            if (keyHeldDelegates.TryGetValue(key, out foo))
            {
                keyHeldDelegates[key] += keyHeldDelegate;
            }
            else
            {
                keyHeldDelegates.Add(key, keyHeldDelegate);
            }
        }
        public void SubscribeAxisDelegegate(InputAxes axis, AxisDelegate axisDelegate)
        {
            AxisDelegate foo;
            if (axisDelegates.TryGetValue(axis, out foo))
            {
                axisDelegates[axis] += axisDelegate;
            }
            else
            {
                axisDelegates.Add(axis, axisDelegate);
            }
        }

        public bool ToggleSiezeMouse()
        {
            SiezeMouse = !SiezeMouse;
            return SiezeMouse;
        }
        public void Start()
        {
            Axes.Add(InputAxes.MoveForward, new Axis
            {
                weights = new List<Func<float>>
                {
                    delegate() { return currentKeyboardState.IsKeyDown(Keys.W) ? 1f : 0f; },
                    delegate() { return currentKeyboardState.IsKeyDown(Keys.S) ? -1f : 0f; }
                }
            });
            Axes.Add(InputAxes.MoveRight, new Axis
            {
                weights = new List<Func<float>>
                {
                    delegate() { return currentKeyboardState.IsKeyDown(Keys.A) ? 1f : 0f; },
                    delegate() { return currentKeyboardState.IsKeyDown(Keys.D) ? -1f : 0f; }
                }
            });
            Axes.Add(InputAxes.MoveUp, new Axis
            {
                weights = new List<Func<float>>
                {
                    delegate() { return currentKeyboardState.IsKeyDown(Keys.Space) ? 1f : 0f; },
                    delegate() { return currentKeyboardState.IsKeyDown(Keys.LeftShift) ? -1f : 0f; }
                }
            });
            Axes.Add(InputAxes.MouseY, new Axis
            {
                weights = new List<Func<float>>
                {
                    delegate() { return deltaMousePosition.Y; }
                }
            });
            Axes.Add(InputAxes.MouseX, new Axis
            {
                weights = new List<Func<float>>
                {
                    delegate() { return deltaMousePosition.X; }
                }
            });
        }
        public void Update(float deltaTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousMousePosition = previousMouseState.Position.ToVector2() / viewportDimensions;
            currentMousePosition = currentMouseState.Position.ToVector2() / viewportDimensions;
            deltaMousePosition = previousMousePosition - currentMousePosition;

            foreach (var entry in keyDownEvents)
            {
                if (currentKeyboardState.IsKeyDown(entry.Key) && !previousKeyboardState.IsKeyDown(entry.Key))
                {
                    entry.Value();
                }
            }
            foreach (var entry in keyUpEvents)
            {
                if (!currentKeyboardState.IsKeyDown(entry.Key) && previousKeyboardState.IsKeyDown(entry.Key))
                {
                    entry.Value();
                }
            }
            foreach (var entry in keyHeldDelegates)
            {
                if (currentKeyboardState.IsKeyDown(entry.Key) && previousKeyboardState.IsKeyDown(entry.Key))
                {
                    entry.Value(deltaTime);
                }
            }
            foreach (var entry in axisDelegates)
            {
                if (Axes.ContainsKey(entry.Key))
                    entry.Value(Axes[entry.Key].GetValue(), deltaTime);
            }

            if(siezeMouse)
            {
                Mouse.SetPosition((int)viewportDimensions.X / 2, (int)viewportDimensions.Y / 2);
                currentMouseState = Mouse.GetState();
            }
        }
    }
}
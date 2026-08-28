---
name: Controle de Chips Visual Identity
colors:
  surface: '#f8f9ff'
  surface-dim: '#d8dae0'
  surface-bright: '#f8f9ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f2f3fa'
  surface-container: '#ecedf4'
  surface-container-high: '#e7e8ee'
  surface-container-highest: '#e1e2e8'
  on-surface: '#191c20'
  on-surface-variant: '#414750'
  inverse-surface: '#2e3035'
  inverse-on-surface: '#eff0f7'
  outline: '#727782'
  outline-variant: '#c1c7d2'
  surface-tint: '#1260a5'
  primary: '#004277'
  on-primary: '#ffffff'
  primary-container: '#005a9e'
  on-primary-container: '#b1d1ff'
  inverse-primary: '#a2c9ff'
  secondary: '#605e5c'
  on-secondary: '#ffffff'
  secondary-container: '#e6e2df'
  on-secondary-container: '#666462'
  tertiary: '#404140'
  on-tertiary: '#ffffff'
  tertiary-container: '#575858'
  on-tertiary-container: '#cfcecd'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#d3e4ff'
  primary-fixed-dim: '#a2c9ff'
  on-primary-fixed: '#001c38'
  on-primary-fixed-variant: '#004881'
  secondary-fixed: '#e6e2df'
  secondary-fixed-dim: '#c9c6c4'
  on-secondary-fixed: '#1c1b1a'
  on-secondary-fixed-variant: '#484645'
  tertiary-fixed: '#e3e2e1'
  tertiary-fixed-dim: '#c7c6c5'
  on-tertiary-fixed: '#1a1c1c'
  on-tertiary-fixed-variant: '#464746'
  background: '#f8f9ff'
  on-background: '#191c20'
  surface-variant: '#e1e2e8'
typography:
  display:
    fontFamily: Work Sans
    fontSize: 28px
    fontWeight: '600'
    lineHeight: 36px
  headline:
    fontFamily: Work Sans
    fontSize: 20px
    fontWeight: '600'
    lineHeight: 28px
  title-md:
    fontFamily: Work Sans
    fontSize: 16px
    fontWeight: '600'
    lineHeight: 24px
  body-lg:
    fontFamily: Work Sans
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  body-md:
    fontFamily: Work Sans
    fontSize: 13px
    fontWeight: '400'
    lineHeight: 18px
  label-sm:
    fontFamily: Work Sans
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.5px
  data-mono:
    fontFamily: JetBrains Mono
    fontSize: 12px
    fontWeight: '400'
    lineHeight: 16px
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  sidebar_width: 240px
  header_height: 64px
  gutter: 16px
  margin_page: 24px
  stack_sm: 8px
  stack_md: 16px
  grid_row_height: 40px
---

## Brand & Style
This design system is engineered for a Windows WPF environment, prioritizing high-density information management and professional reliability. The aesthetic is **Corporate Modern**, characterized by a flat, structured layout that feels native to the Windows ecosystem while maintaining a distinct, premium identity. 

The system utilizes a "Silenced Background" strategy where the UI chrome recedes into light grays and whites, allowing the primary corporate blue and semantic status indicators to drive the user's focus toward actionable data. It avoids complex glass effects or skeuomorphism in favor of crisp borders, clear hit areas, and systematic alignment.

## Colors
The palette is rooted in the **Deep Corporate Blue (#005A9E)**, used strictly for primary actions and brand presence. 

- **Surface Layers:** The main window uses a light gray (#F3F2F1) background to create a subtle contrast against white (#FFFFFF) content cards and data grids.
- **Typography:** The Dark Slate (#201F1E) is the standard for body text, ensuring AAA accessibility and reducing eye strain during prolonged use.
- **Semantic Accents:** 
  - **Active/Stock:** Forest Green for positive status.
  - **In Use:** Deep Amber for attention without urgency.
  - **Damaged/Lost:** Crimson Red for critical errors or loss.
  - **WhatsApp/Communication:** A secondary Blue tint to distinguish connectivity features.

## Typography
The system employs **Work Sans** as the primary typeface. It is a highly legible, professional sans-serif that excels in desktop applications where vertical space is at a premium.

- **Scale:** Font sizes are kept slightly smaller (13px base) to accommodate the dense data requirements of chip management.
- **Hierarchy:** Bold weights (600) are used exclusively for headers and primary navigation labels.
- **Monospaced Data:** For ICCID numbers, phone numbers, or technical codes, a secondary monospaced font (**JetBrains Mono**) is used within data grids to ensure character alignment and readability.

## Layout & Spacing
This design system follows a **Fixed Sidebar + Fluid Content** model optimized for 1080p and 1440p desktop displays.

- **Sidebar:** Fixed at 240px. It contains the primary navigation icons and labels.
- **Header:** A 64px persistent horizontal bar that displays the current screen title and global search/user profile.
- **The 8px Grid:** All internal spacing (padding, margins, gaps) must be a multiple of 8px. 
- **Data Grids:** Designed for efficiency, row heights are set to a compact 40px with 12px horizontal cell padding to maximize the number of visible records without sacrificing touch/click targets.

## Elevation & Depth
In line with modern WPF "Flat" principles, depth is conveyed through **Tonal Layering** and **Low-Contrast Outlines** rather than heavy shadows.

- **Level 0 (Background):** Light Gray (#F3F2F1) — the foundation of the application.
- **Level 1 (Surface):** White (#FFFFFF) — used for the Sidebar and Data Grid containers. These surfaces use a 1px solid border (#EDEBE9).
- **Level 2 (Modals/Popups):** White (#FFFFFF) with a very soft, 8px blur shadow (Opacity 10%, Black) to provide a subtle lift from the main interface during form entry.
- **Focus State:** Elements receive a 2px Primary Blue (#005A9E) border when focused via keyboard or click.

## Shapes
The design system utilizes a **Soft (4px)** corner radius. This provides a professional, modern feel that is less aggressive than sharp corners but more structured than "bubbly" mobile designs.

- **Buttons & Inputs:** 4px radius.
- **Data Grid Container:** 4px radius on the outer wrapper.
- **Status Pills:** Fully rounded (pill-shaped) to distinguish them from interactive buttons.
- **Selection Indicators:** Vertical bars on the left of active sidebar items use 0px rounding for a sharp, precise look.

## Components

### Side Navigation
- **Active State:** Primary Blue text and icon with a 4px wide vertical "accent bar" on the extreme left.
- **Hover State:** Light Gray (#F3F2F1) background fill across the entire width of the item.

### Action Bars
- Positioned directly above data grids. Contains "Add New", "Export", and "Filter" buttons.
- Secondary actions use "Ghost" button styles (Border only, no fill) to maintain hierarchy.

### Data Grids
- **Header:** Medium Gray (#F3F2F1) background with Bold 12px uppercase labels.
- **Striping:** Subtle zebra-striping is optional; preferred is a 1px bottom border on every row.
- **Status Indicators:** Small colored dots or "Pills" with low-opacity background fills (e.g., Active = Light Green background + Dark Green text).

### Input Fields
- Understated 1px gray border. On focus, the border thickens and changes to Primary Blue.
- Labels are always visible above the field in **label-sm** style.

### Modals & Forms
- Center-screen alignment. 
- Form fields are organized in two-column layouts to reduce vertical scrolling.
- "Cancel" and "Save" buttons are always anchored to the bottom right of the modal.